using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageGram.API.Interfaces
{
    public interface IImageWriter
    {
        /// <summary>
        /// Method to upload image 
        /// </summary>
        /// <param name="file">Image with correct image extension</param>
        /// <returns>Image url located in disk</returns>
        Task<string> UploadImage(IFormFile file);
    }
}