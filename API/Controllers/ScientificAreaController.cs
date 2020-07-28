using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.ScientificAreaDTOs;
using Logics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/scientificArea")]
    [ApiController]
    public class ScientificAreaController : ControllerBase
    {
        private readonly IScientificArea _areaLogic;
        private readonly IMapper _mapper;

        public ScientificAreaController(IScientificArea areaLogic, IMapper mapper)
        {
            _areaLogic = areaLogic;
            _mapper = mapper;
        }

        // GET: api/scientificArea
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var areasFromRepo = await _areaLogic.GetObjects();
            if (areasFromRepo == null)
                return NotFound();

            var areaList = _mapper.Map<List<ScientificAreaForListDTO>>(areasFromRepo);
            return Ok(areaList);
        }

        // GET: api/scientificArea/{scientificAreaId}
        [HttpGet("{scientificAreaId}")]
        public async Task<IActionResult> Get(long scientificAreaId)
        {
            var areaFromRepo = await _areaLogic.GetById(scientificAreaId);
            if (areaFromRepo == null)
                return NotFound($"Area with id: {scientificAreaId} does not exist");

            var areaForReturn = _mapper.Map<ScientificAreaDetailsDTO>(areaFromRepo);
            return Ok(areaForReturn);
        }

        // GET: api/scientificArea/criteria/{criteria}
        [HttpGet("criteria/{criteria}")]
        public async Task<IActionResult> Get(string criteria)
        {
            var areasFromRepo = await _areaLogic.Find(criteria);
            if (areasFromRepo == null)
                return NotFound($"Area for this criteria does not exist");

            var areasForReturn = _mapper.Map<List<ScientificAreaForListDTO>>(areasFromRepo);
            return Ok(areasForReturn);
        }

    }
}
