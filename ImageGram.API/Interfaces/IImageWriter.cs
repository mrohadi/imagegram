using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageGram.API.Interfaces
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile file);
    }
}