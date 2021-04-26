using System.Threading.Tasks;
using ImageGram.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageGram.API.Handler
{
    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var image = await _imageWriter.UploadImage(file);
            return $"https://localhost:4001/images/{image}";
        }
    }
}