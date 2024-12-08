
namespace CodeInk.API.Errors;

public class ApiResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
    public ApiResponse(int statusCode, string? message = null, object? data = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        Data = data;
        Success = statusCode >= 200 && statusCode < 300;
    }

    private string? GetDefaultMessageForStatusCode(int? statusCode)
    {

        return statusCode switch
        {
            404 => "Resource Not Found",
            400 => "Bad Request",
            401 => "You Are Not Authorized",
            500 => "Internal Server Error",
            201 => "Created Successfully",
            _ => null,
        };

    }
}
