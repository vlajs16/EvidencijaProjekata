using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.ExternalMentorDTOs;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/externalmentor/")]
    [ApiController]
    [Authorize]
    public class ExternalMentorController : ControllerBase
    {
        private readonly IExternalMentorLogic _logic;
        private readonly IMapper _mapper;
        public ExternalMentorController(IExternalMentorLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }
        // GET: api/ExternalMentor
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var mentors = await _logic.GetObjects();
            if (mentors == null)
                return NotFound();
            var mentorsForReturn = _mapper.Map<List<ExternalMentorForListDTO>>(mentors);
            return Ok(mentorsForReturn);
        }

        // GET: api/ExternalMentor/5
        [HttpGet("{mentorId}")]
        public async Task<IActionResult> Get(int mentorId)
        {
            var mentor = await _logic.GetByID(mentorId);
            if (mentor == null)
                return NotFound("Mentor with id doesn't exist");

            var mentorForReturn = _mapper.Map<ExternalMentorToViewDTO>(mentor);
            return Ok(mentorForReturn);
        }

        // GET: api/ExternalMentor/name/Ivana
        [HttpGet("name/{value}")]
        public async Task<IActionResult> Get(string value)
        {
            var mentors = await _logic.Find(value);
            if (mentors == null)
                return NotFound("Mentors for this criteria don't exist");

            var mentorsForReturn = _mapper.Map<List<ExternalMentorToViewDTO>>(mentors);
            return Ok(mentorsForReturn);
        }

        // POST: api/ExternalMentor
        [HttpPost("{companyId}")]
        public async Task<IActionResult> Post(long companyId, [FromBody] ExternalMentorToInsertDTO mentor)
        {
            if (companyId != long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var mentorForInsert = _mapper.Map<ExternalMentor>(mentor);
            mentorForInsert.CompanyID = companyId;

            if (!await _logic.Insert(mentorForInsert))
                return BadRequest();
            var metorToReturn = _mapper.Map<ExternalMentorToViewDTO>(mentorForInsert);
            return Ok(metorToReturn);
        }

        // PUT: api/ExternalMentor/5
        [HttpPut("{companyId}/{mentorId}")]
        public async Task<IActionResult> Put(int companyId, int mentorId, [FromBody] ExternalMentor mentor)
        {
            mentor.MentorID = mentorId;
            mentor.CompanyID = companyId;
            if (!await _logic.Update(mentor))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{companyId}/{mentorId}")]
        public async Task<IActionResult> Delete(int companyId, int mentorId)
        {
            if (!await _logic.Delete(companyId,mentorId))
                return BadRequest();
            return Ok();
        }
    }
}
