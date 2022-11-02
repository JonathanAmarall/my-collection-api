using Microsoft.AspNetCore.Mvc;
using MyCollection.Api.Dto;
using MyCollection.Domain;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Dto;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Handler;
using MyCollection.Domain.Repositories;

namespace MyCollection.Api.Controllers
{
    [Route("api/v1/collection-items")]
    [ApiController]
    public class CollectionItemController : MainController
    {
        [HttpGet]
        public async Task<ActionResult<PagedList<CollectionItem>>> Get(
            [FromServices] ICollectionItemRepository collectionItemRepository,
            [FromQuery] QueryCollectionItemDto query)
        {
            try
            {
                PagedList<CollectionItem> items = await collectionItemRepository.GetAllPagedAsync(query.GlobalFilter, query.SortOrder, query.SortField, query.Status, query.Type, query.PageNumber, query.PageSize);
                return Ok(items);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CreateCollectionItemCommand command, [FromServices] CollectionItemHandler handler)
        {
            var result = (CommandResult)await handler.HandleAsync(command);
            if (!result.Success)
            {
                result.ValidationResult?.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));
                AddProcessingError(result.Message);
            }

            return CustomReponse(result.Message);
        }

        [HttpPost("{id:guid}/lend")]
        public async Task<ActionResult> Post(Guid id, [FromBody] LendCollectionItemCommand command, [FromServices] CollectionItemHandler handler)
        {
            if (id != command.CollectionItemId)
                return BadRequest();

            var result = (CommandResult)await handler.HandleAsync(command);
            if (!result.Success)
            {
                result.ValidationResult?.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));
                AddProcessingError(result.Message);
            }

            return CustomReponse(new { Message = result.Message });
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> AddLocation(Guid id, [FromBody] AddLocationInCollectionItemCommand command, [FromServices] CollectionItemHandler handler)
        {
            if (id != command.CollectionItemId)
                return BadRequest();

            var result = (CommandResult)await handler.HandleAsync(command);
            if (!result.Success)
            {
                result.ValidationResult?.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));
                AddProcessingError(result.Message);
            }

            return CustomReponse(new { Message = result.Message });
        }

        [HttpGet("{id:guid}/location")]
        public async Task<ActionResult<string>> GetFullLocation(Guid id, [FromServices] ILocationRepository locationRepository)
        {
            return Ok(new { Location = await locationRepository.GetFullLocationTag(id) });
        }

        [HttpGet("contacts")]
        public async Task<ActionResult> GetContacts(
            [FromServices] ICollectionItemRepository collectionItemRepository,
            string? globalFilter, int pageNumber = 1, int pageSize = 5)
        {
            return Ok(await collectionItemRepository.GetAllContactsPagedAsync(globalFilter, pageNumber, pageSize));
        }



    }
}
