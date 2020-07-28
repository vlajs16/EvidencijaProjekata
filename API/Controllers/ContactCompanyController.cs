using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.CompanyContactDTOs;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/contactCompany/{companyId}")]
    [ApiController]
    public class ContactCompanyController : ControllerBase
    {
        private readonly IContactCompany _contactLogic;
        private readonly IMapper _mapper;
        private readonly IEmployeeLogic _employeeLogic;

        public ContactCompanyController(IContactCompany contactLogic, IMapper mapper,
            IEmployeeLogic employeeLogic)
        {
            _contactLogic = contactLogic;
            _mapper = mapper;
            _employeeLogic = employeeLogic;
        }

        // Samo zaposleni koji je ulogovan može da dobije sve kontakte određene kompanije
        // Kasnije se može dodati da i kompanija može videti sve svoje kontakte
        // GET: api/contactCompany/{companyId}/employee/{employeeId}
        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> Get(long companyId, long employeeId)
        {
            if (!await CheckingOnAuth(employeeId))
                return Unauthorized("You are not logged in");


            var contactsFromRepo = await _contactLogic.GetObjects(companyId);
            if (contactsFromRepo == null)
                return BadRequest();

            var contactsToReturn = _mapper.Map<List<ContactToViewDTO>>(contactsFromRepo);
            return Ok(contactsToReturn);
        }

        // Isto se proverava da li je zaposleni odgovarajuci
        // GET: api/contactCompany/{companyId}/{contactId}/employee/{employeeId}
        [HttpGet("{contactId}/employee/{employeeId}")]
        public async Task<IActionResult> Get(long companyId, long contactId, long employeeId)
        {
            if (!await CheckingOnAuth(employeeId))
                return Unauthorized("You are not logged in");

            var contactFromRepo = await _contactLogic.GetByIds(companyId, contactId);
            if (contactFromRepo == null)
                return BadRequest();

            var contactToReturn = _mapper.Map<ContactToViewDTO>(contactFromRepo);
            return Ok(contactToReturn);
            
        }

        // Samo zaposleni može da dodaje kontakt novi za neku kompaniju
        // POST: api/ContactCompany/{companyId}/employee/{employeeId}
        [HttpPost("employee/{employeeId}")]
        public async Task<IActionResult> Post(long companyId, long employeeId, [FromBody] ContactToInsertDTO contactToInsert)
        {
            if(!await CheckingOnAuth(employeeId))
                return Unauthorized("You are not logged in");

            var contactForRepo = _mapper.Map<Contact>(contactToInsert);
            contactForRepo.CompanyID = companyId;

            if (!await _contactLogic.Insert(contactForRepo))
                return BadRequest();
            return Ok();
        }

        // Izmena kontakta
        // PUT: api/ContactCompany/{companyId}/{contactId}/employee/{employeeId}
        [HttpPut("{contactId}/employee/{employeeId}")]
        public async Task<IActionResult> Put(long companyId, long contactId, long employeeId, [FromBody] ContactToInsertDTO contact)
        {
            if (!await CheckingOnAuth(employeeId))
                return Unauthorized("You are not logged in");

            var contactToInsert = _mapper.Map<Contact>(contact);
            contactToInsert.CompanyID = companyId;
            contactToInsert.ContactID = contactId;

            if (!await _contactLogic.Update(contactToInsert))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{contactId}/employee/{employeeId}")]
        public async Task<IActionResult> Delete(long companyId, long contactId, long employeeId)
        {
            if (!await CheckingOnAuth(employeeId))
                return Unauthorized("You are not logged in");

            if (!await _contactLogic.Delete(new Contact
            {
                ContactID = contactId,
                CompanyID = companyId
            }))
                return BadRequest();
            return Ok();
        }

        private async Task<bool> CheckingOnAuth(long employeeId)
        {
            if (employeeId != long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return false;

            var employeeFromRepo = await _employeeLogic.GetByID(employeeId);

            if (employeeFromRepo == null)
                return false;

            if (employeeFromRepo.Username != User.FindFirst(ClaimTypes.Name).Value)
                return false;

            return true;
        }
    }
}
