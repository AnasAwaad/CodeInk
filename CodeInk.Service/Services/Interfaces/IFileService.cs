using Microsoft.AspNetCore.Http;

namespace CodeInk.Core.Service;
public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string subFolder);
    Task DeleteFile(string subFolder);
}
