using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.ExternalMentorDTOs;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/externalmentorcontact")]
    [ApiController]
    public class ExternalMentorContactController : ControllerBase
    {
        private readonly IExternalMentorContactLogic _logic;
        private readonly IMapper _mapper;
        public ExternalMentorContactController(IExternalMentorContactLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }
        // GET: api/ExternalMentorContact
        [HttpGet("{mentorId}")]
        public async Task<IActionResult> Get(int mentorId)
        {
            var contacts = await _logic.GetObjectsForMentor(mentorId);
            if (contacts == null)
                return NotFound();
            var contactsForReturn = _mapper.Map<List<ExternalMentorContactDTO>>(contacts);
            return Ok(contactsForReturn);
        }

        // GET: api/ExternalMentorContact/5
        [HttpGet("{mentorId}/{value}", Name = "GetC")]
        public async Task<IActionResult> GetByValue(int mentorId, string value)
        {
            var contacts = await _logic.Find(mentorId,value);
            if (contacts == null)
                return NotFound();
            var contactsForReturn = _mapper.Map<List<ExternalMentorContactDTO>>(contacts);
            return Ok(contactsForReturn);
        }

        // POST: api/ExternalMentorContact
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExternalMentorContact contact)
        {
            if (!await _logic.Insert(contact))
                return BadRequest();
            return Ok();
        }

        // PUT: api/ExternalMentorContact
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ExternalMentorContact contact)
        {
            if (!await _logic.Update(contact))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ExternalMentorContact/5
        [HttpDelete("{serialNumber}")]
        public async Task<IActionResult> Delete(int serialNumber)
        {
            if (!await _logic.Delete(serialNumber))
                return BadRequest();
            return Ok();
        }
    }
}
