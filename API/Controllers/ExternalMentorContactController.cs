using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.ExternalMentorDTOs;
using DataTransferObjects.ExternalMentorDTOs.ExMentorContact;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/externalmentor/{mentorId}/contacts/")]
    [ApiController]
    public class ExternalMentorContactController : ControllerBase
    {
        private readonly IExternalMentorContactLogic _logic;
        private readonly IMapper _mapper;
        private readonly ICompanyLogic _companyLogic;

        public ExternalMentorContactController(IExternalMentorContactLogic logic, IMapper mapper, ICompanyLogic companyLogic)
        {
            _logic = logic;
            _mapper = mapper;
            _companyLogic = companyLogic;
        }
        // GET: api/ExternalMentorContact
        [HttpGet]
        public async Task<IActionResult> Get(int mentorId)
        {
            var contacts = await _logic.GetObjectsForMentor(mentorId);
            if (contacts == null)
                return NotFound();
            var contactsForReturn = _mapper.Map<List<ExternalMentorContactDTO>>(contacts);
            return Ok(contactsForReturn);
        }

        //// GET: api/ExternalMentorContact/5
        //[HttpGet("{serialNumber}")]
        //public async Task<IActionResult> GetByValue(int mentorId, int serialNumber)
        //{
        //    var contacts = await _logic.Find(mentorId, serialNumber);
        //    if (contacts == null)
        //        return NotFound();
        //    var contactsForReturn = _mapper.Map<List<ExternalMentorContactDTO>>(contacts);
        //    return Ok(contactsForReturn);
        //}

        // POST: api/ExternalMentorContact
        [HttpPost("{companyId}")]
        public async Task<IActionResult> Post(int mentorId, long companyId, [FromBody] MentorContactToInsertDTO contact)
        {
            if (companyId != long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var companyFromRepo = _companyLogic.GetByID(companyId);
            var contactToInsert = _mapper.Map<ExternalMentorContact>(contact);

            contactToInsert.ExternalMentorCompanyID = companyId;
            contactToInsert.ExternalMentorMentorID = mentorId;

            if (!await _logic.Insert(contactToInsert))
                return BadRequest();
            var contactToReturn = _mapper.Map<ExternalMentorContactDTO>(contactToInsert);
            return Ok(contactToReturn);
        }

        // PUT: api/ExternalMentorContact
        [HttpPut("{companyId}/{serialNumber}")]
        public async Task<IActionResult> Put(int mentorId, int companyId, int serialNumber, [FromBody] ExternalMentorContact contact)
        {
            if (companyId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if (!await _logic.Update(contact))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ExternalMentorContact/5
        [HttpDelete("{companyId}/{serialNumber}")]
        public async Task<IActionResult> Delete(int mentorId, int companyId, int serialNumber)
        {
            if (companyId != long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

            if (!await _logic.Delete(mentorId, companyId, serialNumber))
                return BadRequest();
            return Ok();
        }
    }
}
