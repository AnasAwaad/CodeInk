using CodeInk.Core.Service;
using Microsoft.AspNetCore.Http;


namespace CodeInk.Service;
public class FileService : IFileService
{
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
    private readonly long _maxFileSize = 2 * 1024 * 1024;  // 2MB

    public async Task<string> UploadFileAsync(IFormFile file, string folderPath)
    {
        if (file is null)
            throw new ArgumentNullException("No File Uploaded");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Get file extension and ensure it's allowed
        string fileExtension = Path.GetExtension(file.FileName);

        if (!_allowedExtensions.Contains(fileExtension.ToLower()))
            throw new InvalidOperationException("Invalid file type.");

        // Create a unique filename
        string fileName = Guid.NewGuid().ToString() + fileExtension;


        var filePath = Path.Combine(folderPath, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return Path.Combine("Images", "Books", fileName);
    }

    public Task DeleteFile(string folderPath)
    {
        if (File.Exists(folderPath))
        {
            File.Delete(folderPath);
        }
        return Task.CompletedTask;
    }
}
