using Microsoft.AspNetCore.Mvc;
using MyCollection.Api.Dto;
using MyCollection.Domain;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Contracts;
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
        public async Task<ActionResult<CollectionItemPaged<CollectionItem>>> Get(
            [FromServices] ICollectionItemRepository collectionItemRepository,
            [FromQuery] QueryCollectionItemDto query)
        {
            try
            {
                CollectionItemPaged<CollectionItem> items = await collectionItemRepository.GetAllPagedAsync(query.GlobalFilter, query.SortOrder, query.SortField, query.Status, query.Type, query.PageNumber, query.PageSize);
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

            return CustomReponse(result.Message);
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

            return CustomReponse(result.Message);
        }


        private bool UploadArquivo(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                AddProcessingError("Forneça uma imagem para este Item!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imgNome);

            if (System.IO.File.Exists(filePath))
            {
                AddProcessingError("Já exite um arquivo com este nome!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }
    }
}
