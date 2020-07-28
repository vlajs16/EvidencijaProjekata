using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/position")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private IPositionLogic _positionLogic;
        public PositionController(IPositionLogic positionLogic)
        {
            _positionLogic = positionLogic;
        }
        // GET: api/Position
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _positionLogic.GetObjects());
        }

        // GET: api/Position/5
        [HttpGet("{id}", Name = "GetP")]
        public async Task<IActionResult> Get(int id)
        {
            Position p = await _positionLogic.Find(id);
            if (p == null)
                return NotFound($"Pozicija sa id-em: {id} ne postoji!");
            return Ok(p);
        }

        // GET: api/Position/prof
        [HttpGet("name/{value}", Name = "GetByNameP")]
        public async Task<IActionResult> Get(string value)
        {
            List<Position> positions = await _positionLogic.FindByName(value);
            if (positions == null)
                return NotFound($"Pozicije ne postoje.");
            return Ok(positions);
        }

        // POST: api/Position
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Position position)
        {
            if (!await _positionLogic.Insert(position))
                return BadRequest();
            return Ok();
        }

        // PUT: api/Position/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Position position)
        {
            position.PositionID = id;
            if (!await _positionLogic.Update(position))
                return BadRequest();
            return Ok();
        }
    }
}
