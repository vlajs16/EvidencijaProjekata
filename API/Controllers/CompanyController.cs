using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.CompanyDTOs;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyLogic _companyLogic;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyLogic companyLogic, IMapper mapper)
        {
            _companyLogic = companyLogic;
            _mapper = mapper;
        }

        // GET: api/Company
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var companiesFromRepo = await _companyLogic.GetObjects();
            if (companiesFromRepo == null)
                return NotFound();
            var companiesToReturn = _mapper.Map<List<CompanyForListDTO>>(companiesFromRepo);
            return Ok(companiesToReturn);
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var companyFromRepo = await _companyLogic.GetByID(id);
            if (companyFromRepo == null)
                return NotFound();
            var companyToReturn = _mapper.Map<CompanyDetailsDTO>(companyFromRepo);
            return Ok(companyToReturn);
        }

        [HttpGet("criteria/{criteria}")]
        public async Task<IActionResult> Get(string criteria)
        {
            var companiesFromRepo = await _companyLogic.FindObjects(criteria);
            if (companiesFromRepo == null)
                return NotFound();
            var companiesToReturn = _mapper.Map<List<CompanyForListDTO>>(companiesFromRepo);
            return Ok(companiesToReturn);
        }

        // POST: api/Company
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyForInsertDTO company)
        {
            company.Username = company.Username.ToLower();
            var convertedCompany = _mapper.Map<Company>(company);
            if (await _companyLogic.Insert(convertedCompany, company.Password))
                return StatusCode(201);
            return BadRequest("Error");
        }

    }
}
