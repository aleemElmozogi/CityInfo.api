using CityInfo.api.Entities;
using CityInfo.api.Models;

namespace CityInfo.api.Services
{
    public interface ICityInfoReposotiory
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<(IEnumerable<City>, PaginationMetaData)> GetCitiesAsync(string? name, string? searchQuery,int currentPage, int pageSize);
        Task<City?> GetCityAsync(int cityId, bool includePOI);
        Task<CityDto> GetCityAsyncw(int cityId, bool includePOI);

        Task<IEnumerable<PointOfInrest?>> GetPointOfInrestsForCityAsync(int cityId);

        Task<bool> CityExistsAsync(int cityId);
        Task<bool> SaveChangesAsync();
        void DeletePointOfIntrest(PointOfInrest pointOfInrest);
        Task AddPointOfIntrestAsync(int cityId, PointOfInrest pointOfInrest);

        Task<PointOfInrest?> GetPointOfInrestForCityAsync(int cityId, int poitOfInrestId);
    }
}
