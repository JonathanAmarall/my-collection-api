using Microsoft.AspNetCore.Mvc;
using MyCollection.Domain.Contracts;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionItemController : MainController
    {
        [HttpGet]
        public async Task<ActionResult<CollectionItemPaged<CollectionItem>>> Get(string globalFilter, string sortOrder, string sortField, int pageIndex, int pageSize, [FromServices] ICollectionItemRepository collectionItemRepository)
        {
            CollectionItemPaged<CollectionItem> items = await collectionItemRepository.GetAllPagedAsync(globalFilter, sortOrder, sortField, pageIndex, pageSize);
            return Ok(items);
        }
    }
}
