using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyCollection.Api.Dto;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Handler;
using MyCollection.Domain.Repositories;

namespace MyCollection.Api.Controllers
{
    [Route("api/v1/locations")]
    [ApiController]
    public class LocationsController : MainController
    {
        private readonly IMapper _mapper;

        public LocationsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<LocationDto>>> Get([FromServices] ILocationRepository locationRepository)
        {
            var locations = _mapper.Map<List<LocationDto>>(await locationRepository.GetRootsAsync());

            return Ok(locations);
        }

        [HttpGet("{id:guid}/childrens")]
        public async Task<ActionResult<List<LocationDto>>> GetChildrens(Guid id, [FromServices] ILocationRepository locationRepository)
        {
            var locationsRoots = _mapper.Map<List<LocationDto>>(await locationRepository.GetChildrensAsync(id));
            return Ok(locationsRoots);
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
