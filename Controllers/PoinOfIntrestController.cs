using CityInfo.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.api.Controllers
{
    [Route("api/Cities/{cityId}/[controller]")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PoinOfIntrestDTO>> GetPointOfIntrest(int cityId) {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.poinOfIntrest);

        }
        [HttpGet("{pointOfIntrestId}")]
        public ActionResult<PoinOfIntrestDTO> GetPointOfIntrest(int cityId, int pointOfIntrestId)
    {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfIntrest = city.poinOfIntrest.FirstOrDefault(c => c.Id == pointOfIntrestId);
            if(pointOfIntrest== null)
            { 
                return NotFound();
            }
            return Ok(pointOfIntrest);

        }
    }
}
