using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Dto;
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
            List<Location> locationsroots = await locationRepository.GetRootsAsync();
            return Ok(locationsroots);
        }

        [HttpGet("{id:guid}/childrens")]
        public async Task<ActionResult<List<LocationDto>>> GetChildrens(Guid id, [FromServices] ILocationRepository locationRepository)
        {
            // TODO: Puxar entidade e mapear para DTO
            List<LocationDto>? locationsroots = await locationRepository.GetChildrensAsync(id);
            return Ok(locationsroots);
        }

        [HttpGet("{id:guid}/full-location")]
        public async Task<ActionResult> GetFullLocation(Guid id, [FromServices] ILocationRepository locationRepository)
        {
            return Ok(await locationRepository.GetFullLocationTag(id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLocationCommand command, [FromServices] LocationHandler handler)
        {
            var result = (CommandResult)await handler.HandleAsync(command);
            if (!result.Success)
                result.ValidationResult?.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));

            return CustomReponse(result.Data);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, [FromServices] ILocationRepository locationRepository)
        {
            var location = await locationRepository.GetByIdAsync(id);
            if (location == null)
                return BadRequest();

            if (location.HasChildren())
                return BadRequest(new { Message = "Esta localização não pode ser excluída, pois possui Localizações pendentes." });

            if (location.HasCollectionItem())
                return BadRequest(new { Message = "Esta localização não pode ser excluída, pois possui Itens armazenados." });

            locationRepository.Delete(location);
            await locationRepository.UnitOfWork.Commit();

            return NoContent();
        }
    }
}
