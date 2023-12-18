using CityInfo.api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CitiesController: ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities() {
          return  Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var cityToreturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToreturn == null)
            {
                return NotFound();
            }
            return Ok(cityToreturn);
        }

    }
}
 