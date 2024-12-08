using System.Text.Json;

namespace CodeInk.API.Errors;

public class ErrorDetails
{

    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public ErrorDetails()
    {

    }
    public ErrorDetails(int statusCode, string errorMessage, IEnumerable<string>? errors = null)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
        Errors = errors;

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
