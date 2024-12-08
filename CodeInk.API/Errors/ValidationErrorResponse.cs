
namespace CodeInk.API.Errors;

public class ValidationErrorResponse
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
    public IEnumerable<ValidationError> Errors { get; set; }
    public ValidationErrorResponse()
    {
        Errors = new List<ValidationError>();
    }
}

public class ValidationError
{
    public string Field { get; set; }
    public IEnumerable<string> Errors { get; set; }
}
