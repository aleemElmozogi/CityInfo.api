using CityInfo.api.DbContexts;
using CityInfo.api.Entities;
using CityInfo.api.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.api.Services
{
    public class CityInfoReposotory : ICityInfoReposotiory
    {
        private readonly CityInfoContext _context;

        public  CityInfoReposotory(CityInfoContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c=>c.Id).ToListAsync();

        }
        public async Task<(IEnumerable<City>, PaginationMetaData)> GetCitiesAsync(string? name, string? searchQuery,
            int currentPage, int pageSize)
        {
          

            var collection = _context.Cities as IQueryable<City>;

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);

            }
            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(c =>c.Name.Contains(searchQuery)|| (c.Description != null && c.Description.Contains(searchQuery)));


            }

            var totalItemsCount = await collection.CountAsync();
            var paginationMetaData = new PaginationMetaData(totalItemsCount, currentPage, pageSize);
             var collectionToReturn =  await collection.OrderBy(c=>c.Name)
                .Skip(pageSize * (currentPage -1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);    

        }

        public async Task<City?> GetCityAsync(int cityId, bool includePOI)
        {

            if (includePOI) {
                return await _context.Cities.Include(c => c.pointOfInrests).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();

        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c=> c.Id == cityId);
        }

        public async Task<IEnumerable<PointOfInrest?>> GetPointOfInrestsForCityAsync(int cityId)
        {
            return await _context.PointOfInrest.Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task<PointOfInrest?> GetPointOfInrestForCityAsync(int cityId, int poitOfInrestId)
        {
            return await _context.PointOfInrest.Where(p => p.CityId == cityId && p.Id == poitOfInrestId).FirstOrDefaultAsync();
        }
        public async Task AddPointOfIntrestAsync(int cityId, PointOfInrest pointOfInrest)
        {
            //await _context.PointOfInrest.AddAsync(pointOfInrest);
            var city = await GetCityAsync(cityId,false);
            if(city != null)
            {
                city.pointOfInrests.Add(pointOfInrest);
            }

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<CityDto> GetCityAsyncw(int cityId, bool includePOI)
        {
            var dd = _context.Cities.Include(x => x.pointOfInrests).Where(x => x.Id == cityId).Select(x => new CityDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                pointOfInrests = x.pointOfInrests.Select(x => new PoinOfIntrestDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList()
            }).FirstOrDefault();

            return dd;
        }

        public void DeletePointOfIntrest(PointOfInrest pointOfInrest) { 
        _context.PointOfInrest.Remove(pointOfInrest);
        
        }
       



    }
}
