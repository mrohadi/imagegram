using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageGram.API.Interfaces
{
    public interface IImageHandler
    {
        /// <summary>
        /// Upload image to the disk
        /// </summary>
        /// <param name="file">Image file upload</param>
        /// <returns>Image url</returns>
        Task<string> UploadImage(IFormFile file);
    }
}