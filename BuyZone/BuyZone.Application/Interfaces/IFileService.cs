using Microsoft.AspNetCore.Http;

namespace BuyZone.Application.Interfaces;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string folderName);
}