using MyCollection.Api.Dto;
using MyCollection.Domain.Entities;

namespace MyCollection.Api
{

    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {

            CreateMap<LocationDto, Location>()
                .ReverseMap();

        }
    }
}