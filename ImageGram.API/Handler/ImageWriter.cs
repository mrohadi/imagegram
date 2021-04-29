using System;
using System.IO;
using System.Threading.Tasks;
using ImageGram.API.Helper;
using ImageGram.API.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ImageGram.API.Handler
{
    public class ImageWriter : IImageWriter
    {
        public async Task<string> UploadImage(IFormFile file)
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file);
            }

            return "Invalid image file";
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file">Image file to check</param>
        /// <returns>True if the all of the conditions are met</returns>
        private static bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file">Image file to write</param>
        /// <returns>Image url in static folder</returns>
        public async Task<string> WriteFile(IFormFile file)
        {
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[^1];
                fileName = Guid.NewGuid().ToString() + extension; 

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using var bits = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(bits);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return $"https://localhost:4001/images/{fileName}";
        }
    }
}