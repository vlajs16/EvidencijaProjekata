using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.LocationDTOs;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/location/{companyId}")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationLogic _location;
        private readonly IMapper _mapper;

        public LocationController(ILocationLogic location, IMapper mapper)
        {
            _location = location;
            _mapper = mapper;
        }
        // GET: api/location/{companyId}
        [HttpGet]
        public async Task<IActionResult> Get(long companyId)
        {
            var locationsFromRepo = await _location.GetObjects(companyId);
            if (locationsFromRepo == null)
                return NotFound("There is no location for this company");

            var locationsToReturn = _mapper.Map<List<LocationToViewCompanyDTO>>(locationsFromRepo);
            return Ok(locationsToReturn);
        }

        // GET: api/location/{companyId}/city/{cityId}
        [HttpGet("city/{cityId}")]
        public async Task<IActionResult> Get(long companyId, long cityId)
        {
            var locationFromRepo = await _location.GetById(companyId, cityId);
            if (locationFromRepo == null)
                return NotFound("We could not found this location");

            var locationToReturn = _mapper.Map<LocationToViewCompanyDTO>(locationFromRepo);
            return Ok(locationToReturn);
        }

        // POST: api/location/{companyId}
        [HttpPost]
        public async Task<IActionResult> Post(long companyId, [FromBody] LocationInsertCompanyDTO location)
        {
            var locationToInsert = _mapper.Map<Location>(location);
            locationToInsert.CompanyID = companyId;

            if (!await _location.Insert(locationToInsert))
                return BadRequest();

            return StatusCode(201);
        }

        // PUT: api/location/{companyId}/city/{cityId}
        [HttpPut("city/{cityId}")]
        public async Task<IActionResult> Put(long companyId, long cityId, [FromBody] LocationToUpdateDTO location)
        {
            var locationToUpdate = _mapper.Map<Location>(location);
            locationToUpdate.CompanyID = companyId;
            locationToUpdate.CityID = cityId;

            if (!await _location.Update(locationToUpdate))
                return BadRequest();

            return Ok();
        }

        // DELETE: api/location/{companyId}/city/{cityId}
        [HttpDelete("city/{cityId}")]
        public async Task<IActionResult> Delete(long companyId, long cityId)
        {
            if (!await _location.Delete(companyId, cityId))
                return BadRequest();
            return Ok();
        }
    }
}
