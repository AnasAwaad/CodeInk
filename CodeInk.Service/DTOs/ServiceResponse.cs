namespace CodeInk.Application.DTOs;
public record ServiceResponse(bool success = false, string? message = null, ServiceErrorCode errorCode = ServiceErrorCode.None);
public enum ServiceErrorCode
{
    None = 0,
    NotFound = 404,
    BadRequest = 400,
    Conflict = 409,
    InternalError = 500,
    Success = 200,
    Created = 201
}