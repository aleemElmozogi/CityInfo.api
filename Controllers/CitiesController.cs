using AutoMapper;
using CityInfo.api.Models;
using CityInfo.api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoReposotiory _cityInfoRepsotory;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoReposotiory cityInfoRepsotory, IMapper mapper) {
            _cityInfoRepsotory = cityInfoRepsotory ?? throw new ArgumentNullException(nameof(CitiesDataStore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(CitiesDataStore));

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityOnlyDto>>> GetCities() {

            var cityEntities = await _cityInfoRepsotory.GetCitiesAsync();


            return Ok(_mapper.Map<IEnumerable<CityOnlyDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCity(int id, bool includePOI = false)
        {
            var cityToreturn = await _cityInfoRepsotory.GetCityAsync(id, includePOI);

            if (cityToreturn == null)
            {
                return NotFound();
            }

            var kk = _mapper.Map<CityDto>(cityToreturn);
            if (includePOI)
            {
                return Ok(kk);

            }
            return Ok(_mapper.Map<CityOnlyDto>(cityToreturn));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCitysw(int id, bool includePOI = false)
        {

            var cityToreturn = await _cityInfoRepsotory.GetCityAsyncw(id, includePOI);

            if (cityToreturn == null)
            {
                return NotFound();
            }

           
            return Ok(cityToreturn);
        }

} }
 