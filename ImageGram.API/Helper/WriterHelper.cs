using System.Linq;
using System.Text;

namespace ImageGram.API.Helper
{
    public class WriterHelper
    {
        public enum ImageFormat
        {
            bmp,
            jpeg,
            png,
            unknown
        }

        /// <summary>
        /// Get image format or extension
        /// </summary>
        /// <param name="bytes">Image bytes</param>
        /// <returns>Image format</returns>
        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");
            var png = new byte[] { 137, 80, 78, 71 };
            var jpeg = new byte[] { 255, 216, 255, 224 };

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;
        }
    }
}