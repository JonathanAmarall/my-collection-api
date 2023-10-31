using Microsoft.AspNetCore.Mvc;
using MyCollection.Api.Models.Request;
using MyCollection.Application.Borrower.Commands.CreateBorrower;
using MyCollection.Core.Messages.Commands;
using MyCollection.Core.Models;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Api.Controllers
{
    [Route("api/v1/borrowers")]
    [ApiController]
    public class BorrowersController : MainController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(
            [FromServices] IBorrowerRepository borrowerRepository,
            [FromQuery] GetAllBorrowersPagedQueryRequest queryRequest)
        {
            var query = await borrowerRepository.GetAllPagedAsync(
                queryRequest.GlobalFilter,
                queryRequest.SortOrder,
                queryRequest.SortField,
                queryRequest.PageNumber,
                queryRequest.PageSize);

            return Ok(query);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Create(
            [FromBody] CreateBorrowerCommand command,
            [FromServices] CreateBorrowerCommandHandler handler)
        {
            var result = (CommandResult<Borrower>)await handler.HandleAsync(command);

            if (result.IsFailure)
            {
                AddProcessingErrors(command.ValidationResult!);
            }

            return CustomReponse();
        }
    }
}
