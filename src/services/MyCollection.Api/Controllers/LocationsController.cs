using Microsoft.AspNetCore.Mvc;
using MyCollection.Core.Messages.Commands;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Handler;
using MyCollection.Domain.Repositories;

namespace MyCollection.Api.Controllers
{
    [Route("api/v1/locations")]
    [ApiController]
    public class LocationsController : MainController
    {
        [HttpGet]
        public async Task<ActionResult<List<Location>>> Get([FromServices] ILocationRepository locationRepository)
        {
            var locations = await locationRepository.GetRootsAsync();

            return Ok(locations);
        }

        [HttpGet("{id:guid}/children")]
        public async Task<ActionResult<List<Location>>> GetChildren(Guid id,
            [FromServices] ILocationRepository locationRepository)
        {
            var locationsRoots = await locationRepository.GetLocationsChildrenAsync(id);
            return Ok(locationsRoots);
        }

        [HttpGet("{id:guid}/full-location")]
        public async Task<ActionResult> GetFullLocation(Guid id, [FromServices] ILocationRepository locationRepository)
        {
            return Ok(await locationRepository.GetFullLocationTag(id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLocationCommand command,
            [FromServices] CreateLocationCommandHandler handler)
        {
            var result = (CommandResult<Location>)await handler.HandleAsync(command);
            if (result.IsFailure)
            {
                result.ValidationResult?.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));
            }

            return CustomReponse(result.Data);
        }

        // TODO: Mover para Command/Handler
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, [FromServices] ILocationRepository locationRepository)
        {
            var location = await locationRepository.GetByIdAsync(id);
            if (location is null)
            {
                return NotFound();
            }

            if (location.HasChildren())
            {
                AddProcessingError("Esta localização não pode ser excluída, pois possui Localizações pendentes.");
            }

            if (location.HasCollectionItem())
            {
                AddProcessingError("Esta localização não pode ser excluída, pois possui Itens armazenados.");

            }

            locationRepository.Delete(location);
            await locationRepository.UnitOfWork.Commit();

            return NoContent();
        }
    }
}