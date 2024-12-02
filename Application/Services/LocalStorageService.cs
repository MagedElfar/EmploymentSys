using Core.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class LocalStorageService : IMediaStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string?> UploadAsync(IFormFile file, string folderName)
        {

            if (file == null || file.Length == 0)
                return null;

            // Generate a unique filename and define the path
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
            Directory.CreateDirectory(folderPath); // Ensure the folder exists
            var filePath = Path.Combine(folderPath, uniqueFileName);

            // Save file locally
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the relative path to the image
            return Path.Combine("/", folderName, uniqueFileName);
        }

        public Task DeleteAsync(string filePath)
        {
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }
    }

}
