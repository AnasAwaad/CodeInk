using CodeInk.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Controllers;
[Route("errors/{code}")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    public ActionResult Error(int code)
    {
        return NotFound(new ApiResponse(code));
    }
}
