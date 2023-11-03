using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyCollection.Core.Models;
using System.Security.Claims;


namespace MyCollection.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ApiErrorResponse ApiErrorResponse { get; private set; } = new();

        protected ActionResult CustomReponse(object result = null!)
        {
            if (OperationValid())
            {
                return Ok(result);
            }

            return BadRequest(ApiErrorResponse);
        }

        protected ActionResult CustomReponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                AddProcessingError(error.ErrorMessage);
            }

            return CustomReponse();
        }

        protected void AddProcessingError(string error)
        {
            ApiErrorResponse.AddError(error);
        }

        protected void AddProcessingErrors(List<string> errors)
        {
            ApiErrorResponse = new ApiErrorResponse(errors);
        }

        protected void AddProcessingErrors(ValidationResult validationResult)
        {
            if (validationResult is not null)
            {
                validationResult.Errors
                    .ToList().
                    ForEach(e =>
                    AddProcessingError(e.ErrorMessage));
            }
        }

        protected void ClearProcessingErrors()
        {
            ApiErrorResponse.Response.Clear();
        }

        protected bool OperationValid()
        {
            return !ApiErrorResponse.HasErrors();
        }

        protected Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                AddProcessingError("User not found.");

            if (!Guid.TryParse(userId, out Guid result))
                AddProcessingError("User not found.");

            return result;
        }
    }
}
