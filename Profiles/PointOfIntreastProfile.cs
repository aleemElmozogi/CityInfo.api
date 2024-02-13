using AutoMapper;

namespace CityInfo.api.Profiles
{
    public class PointOfIntreastProfile :Profile
    {
        public PointOfIntreastProfile() {
            CreateMap<Entities.PointOfInrest, Models.PoinOfIntrestDTO>();
            CreateMap< Models.PointOfIntrestCreationDto, Entities.PointOfInrest>();
        }
    }
}
