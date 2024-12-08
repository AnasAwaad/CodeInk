using System.Text.Json;

namespace CodeInk.API.Errors;

public class ErrorDetails
{

    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }

    public ErrorDetails(int statusCode, string errorMessage)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }

    public override string ToString()
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return JsonSerializer.Serialize(this, options);
    }
}
