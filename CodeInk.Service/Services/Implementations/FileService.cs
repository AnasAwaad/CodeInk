using CodeInk.Core.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace CodeInk.Application.Services.Implementations;
public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
    private readonly long _maxFileSize = 2 * 1024 * 1024;  // 2MB

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }


    public async Task<string> UploadFileAsync(IFormFile file, string destination)
    {

        // Validate the file
        ValidateFile(file);

        // Combine the destination folder with the root path
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, destination);

        // Ensure the directory exists
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        // Create a unique filename
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);


        var filePath = Path.Combine(uploadPath, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return Path.Combine(destination, fileName).Replace("\\", "/");
    }

    private void ValidateFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty.");

        if (file.Length > _maxFileSize)
            throw new ArgumentException($"File size exceeds the maximum limit of {_maxFileSize / (1024 * 1024)} MB.");

        string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!_allowedExtensions.Contains(fileExtension))
            throw new ArgumentException($"File type '{fileExtension}' is not allowed. Allowed types are: {string.Join(", ", _allowedExtensions)}.");
    }

    public void DeleteFile(string filePath)
    {
        string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);

        fullPath = fullPath.Replace("\\", "/");
        if (File.Exists(fullPath))
        {
            try
            {
                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Could not delete the file.", ex);
            }
        }
    }
}
