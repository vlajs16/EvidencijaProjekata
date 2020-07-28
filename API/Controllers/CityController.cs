using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.CityDTOs;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityLogic _cityLogic;
        private readonly IMapper _mapper;

        public CityController(ICityLogic cityLogic, IMapper mapper)
        {
            _cityLogic = cityLogic;
            _mapper = mapper;
        }
        // GET: api/City
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var citiesFromDb = await _cityLogic.GetAll();
            if (citiesFromDb == null)
                return NotFound();

            var citiesToReturn = _mapper.Map<List<CityToListDTO>>(citiesFromDb);
            return Ok(citiesToReturn);
        }

        // GET: api/City/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(float id)
        {
            var cityFromDb = await _cityLogic.GetById(id);
            if (cityFromDb == null)
                return NotFound("City with this id does not exist");

            var cityToReturn = _mapper.Map<CityDetailsDTO>(cityFromDb);
            return Ok(cityToReturn);
        }

        [HttpGet("criteria/{criteria}")]
        public async Task<IActionResult> Get(string criteria)
        {
            var citiesFromDb = await _cityLogic.Find(criteria);
            if (citiesFromDb == null)
                return NotFound("City for this criteria does not exist");

            var citiesToReturn = _mapper.Map<List<CityToListDTO>>(citiesFromDb);
            return Ok(citiesToReturn);
        }
    }
}
