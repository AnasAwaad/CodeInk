using CodeInk.API.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CodeInk.API.Factories;

public class ApiResponseFactory
{
    public static IActionResult CustomeValidationErrorResponse(ActionContext actionContext)
    {
        var errors = actionContext.ModelState.Where(P => P.Value?.Errors.Count > 0)
                                             .Select(e => new ValidationError
                                             {
                                                 Field = e.Key,
                                                 Errors = e.Value.Errors.Select(e => e.ErrorMessage)
                                             })
                                             .ToList();

        var response = new ValidationErrorResponse()
        {
            ErrorMessage = "Validation Field",
            StatusCode = (int)HttpStatusCode.BadRequest,
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    }
}
