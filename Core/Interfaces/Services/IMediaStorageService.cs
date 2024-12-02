using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IMediaStorageService
    {
        Task<string> UploadAsync(IFormFile file, string folderName);
        Task DeleteAsync(string filePath);
    }
}
