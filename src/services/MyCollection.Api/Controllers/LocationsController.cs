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
            List<LocationDto> locationsroots = await locationRepository.GetChildrensAsync(id);
            return Ok(locationsroots);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLocationCommand command, [FromServices] LocationHandler handler)
        {
            var result = (CommandResult)await handler.HandleAsync(command);
            if (!result.Success)
                result.ValidationResult?.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));

            return CustomReponse(result.Message);
        }


    }
}
