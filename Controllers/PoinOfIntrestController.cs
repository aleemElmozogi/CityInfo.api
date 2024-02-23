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
        [HttpPut("{pointOfIntrestId}")]
        public async Task<ActionResult> UpdatePointOfIntrest(int cityId, int pointOfIntrestId, PointOfIntrestForUpdateDto pointOfIntrest)
        {
            if (!await _cityInfoReposotiory.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found");
                return NotFound();
            }

            var pointOfIntrestEntity = await _cityInfoReposotiory.GetPointOfInrestForCityAsync(cityId, pointOfIntrestId);
                if (pointOfIntrestEntity == null)
            {
                return NotFound();
            }
            _mapper.Map(pointOfIntrest, pointOfIntrestEntity);

           await _cityInfoReposotiory.SaveChangesAsync();
      

            return NoContent();

        }
        [HttpPatch("{pointOfIntrestId}")]
        public async Task<ActionResult> PartillyUpdatePointOfIntrest(
            int cityId, int pointOfIntrestId, JsonPatchDocument<PointOfIntrestForUpdateDto> patchDocument
            )
        {

            if (!await _cityInfoReposotiory.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found");
                return NotFound();
            }

            var pointOfIntrestEntity = await _cityInfoReposotiory.GetPointOfInrestForCityAsync(cityId, pointOfIntrestId);
            if (pointOfIntrestEntity == null)
            {
                return NotFound();
            }
            var pointOfIntrestToPatch = _mapper.Map<PointOfIntrestForUpdateDto>(pointOfIntrestEntity);

            patchDocument.ApplyTo(pointOfIntrestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointOfIntrestToPatch, pointOfIntrestEntity);
          await  _cityInfoReposotiory.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{pointOfIntrestId}")]
        public async Task<ActionResult> DeletePointOfIntrest(int cityId, int pointOfIntrestId)
        {

            if (!await _cityInfoReposotiory.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found");
                return NotFound();
            }

            var pointOfIntrestEntity = await _cityInfoReposotiory.GetPointOfInrestForCityAsync(cityId, pointOfIntrestId);
            if (pointOfIntrestEntity == null)
            {
                return NotFound();
            }
          
            _cityInfoReposotiory.DeletePointOfIntrest(pointOfIntrestEntity);

          await  _cityInfoReposotiory.SaveChangesAsync();
            _mailService.Send("Point of intreaset was deleted", $"point of intrest {pointOfIntrestEntity.Name} with id {pointOfIntrestEntity.Id} was deleted");

            return NoContent();
        }

    }
}
 