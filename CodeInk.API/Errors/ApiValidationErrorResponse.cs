using CodeInk.Application.DTOs;

namespace CodeInk.API.Errors;

public class ApiValidationErrorResponse : ApiResponse
{
    public IEnumerable<string> Errors { get; set; }
    public ApiValidationErrorResponse() : base(400)// validation error is bad request
    {
        Errors = new List<string>();
    }
}
