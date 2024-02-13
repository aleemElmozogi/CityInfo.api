using AutoMapper;

namespace CityInfo.api.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile() { 
        CreateMap<Entities.City,Models.CityOnlyDto>();
        CreateMap<Entities.City,Models.CityDto>();
        }
    }
}
