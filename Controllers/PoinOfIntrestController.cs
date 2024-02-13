using AutoMapper;
using CityInfo.api.Models;
using CityInfo.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.api.Controllers
{
    [Route("api/Cities/{cityId}/[controller]")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {

        private readonly ILogger<PointsOfInterestController> _logger;  
        private readonly IMailService _mailService;  
        private readonly ICityInfoReposotiory _cityInfoReposotiory;  
        private readonly IMapper _mapper;  

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService localMailService, ICityInfoReposotiory cityInfoReposotiory, IMapper mapper
            )
        {
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
            _mailService = localMailService?? throw new ArgumentNullException(nameof(localMailService));
            _cityInfoReposotiory = cityInfoReposotiory ?? throw new ArgumentNullException(nameof(ICityInfoReposotiory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));

        }

        [HttpGet]
        public async Task< ActionResult<IEnumerable<PoinOfIntrestDTO>>> GetPointOfIntrest(int cityId)  {
           
            if(!await _cityInfoReposotiory.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found");
                return NotFound();
            }
            var pointOfIntrestForCity = await _cityInfoReposotiory.GetPointOfInrestsForCityAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<PoinOfIntrestDTO>>(pointOfIntrestForCity));
            

        }
        [HttpGet("{pointOfIntrestId}", Name = "GetPointOfIntrest")]
        public async Task<ActionResult<PoinOfIntrestDTO>> GetPointOfIntrest(int cityId, int pointOfIntrestId)
        {
            if (!await _cityInfoReposotiory.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found");
                return NotFound();
            }
            var pointOfIntrest = await _cityInfoReposotiory.GetPointOfInrestForCityAsync(cityId, pointOfIntrestId);
            if (pointOfIntrest == null) {
                return NotFound();
            }
            return Ok(_mapper.Map<PoinOfIntrestDTO>(pointOfIntrest));


        }

        [HttpPost]
        public async Task<ActionResult<PoinOfIntrestDTO>> CreatePointOfIntrest(int cityId, PointOfIntrestCreationDto pointOfIntrest)
        {
            if (!await _cityInfoReposotiory.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found");
                return NotFound();
            }
            var finalPointOfIntreset = _mapper.Map<Entities.PointOfInrest>(pointOfIntrest);
            
             await _cityInfoReposotiory.AddPointOfIntrestAsync(cityId, finalPointOfIntreset);

            await _cityInfoReposotiory.SaveChangesAsync();

            var createdPointOfInterestToRoute  = _mapper.Map<Models.PoinOfIntrestDTO>(finalPointOfIntreset);
            return CreatedAtRoute("GetPointOfIntrest", new
             {
                cityId,
                pointOfIntrestId = createdPointOfInterestToRoute.Id

            }, createdPointOfInterestToRoute);
        }
        //[HttpPut("{pointOfIntrestId}")]
        //public ActionResult UpdatePointOfIntrest(int cityId, int pointOfIntrestId, PointOfIntrestForUpdateDto pointOfIntrest) {
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    var pointOfIntrestFromStore = city.pointOfInrests
        //        .FirstOrDefault(c => c.Id == pointOfIntrestId);
        //    if (pointOfIntrestFromStore == null)
        //    {
        //        return NotFound();
        //    }

        //    pointOfIntrestFromStore.Name= pointOfIntrest.Name;
        //    pointOfIntrest.Description= pointOfIntrest.Description;

        //    return NoContent();

        //}
        //[HttpPatch("{pointOfIntrestId}")]
        //public ActionResult PartillyUpdatePointOfIntrest(
        //    int cityId, int pointOfIntrestId, JsonPatchDocument<PointOfIntrestForUpdateDto> patchDocument
        //    )
        //{

        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    var pointOfIntrestFromStore = city.pointOfInrests
        //        .FirstOrDefault(c => c.Id == pointOfIntrestId);
        //    if (pointOfIntrestFromStore == null)
        //    {
        //        return NotFound();
        //    }
        //    var pointOfIntrestToPatch = new PointOfIntrestForUpdateDto()
        //    {
        //        Name = pointOfIntrestFromStore.Name,
        //        Description = pointOfIntrestFromStore.Description,
        //    };
        //    patchDocument.ApplyTo(pointOfIntrestToPatch, ModelState);
        //    if(!ModelState.IsValid) {  
        //    return BadRequest(ModelState);
        //    }

        //    pointOfIntrestFromStore.Name = pointOfIntrestToPatch.Name;
        //    pointOfIntrestFromStore.Description= pointOfIntrestToPatch.Description;

        //    return NoContent();
        //}
        //[HttpDelete("{pointOfIntrestId}")]
        //public ActionResult DeletePointOfIntrest(int cityId, int pointOfIntrestId) { 

        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    var pointOfIntrestFromStore = city.pointOfInrests
        //        .FirstOrDefault(c => c.Id == pointOfIntrestId);
        //    if (pointOfIntrestFromStore == null)
        //    {
        //        return NotFound();
        //    }
        //    city.pointOfInrests.Remove(pointOfIntrestFromStore);
        //    _mailService.Send("Point of intreaset was deleted", $"point of intrest {pointOfIntrestFromStore.Name} with id {pointOfIntrestFromStore.Id} was deleted");

        //    return NoContent();
        //}

    }
}
 