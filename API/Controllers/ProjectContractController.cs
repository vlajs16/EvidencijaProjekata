using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.ProjectContractDTOs;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/projectContract")]
    [ApiController]
    public class ProjectContractController : ControllerBase
    {
        private readonly IProjectContractLogic _contract;
        private readonly IMapper _mapper;

        public ProjectContractController(IProjectContractLogic contract, IMapper mapper)
        {
            _contract = contract;
            _mapper = mapper;
        }

        // GET: api/projectContract
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contracts = await _contract.GetObjects();
            if (contracts == null)
                return NotFound();

            var contractsToReturn = _mapper.Map<List<ProjectContractForListDTO>>(contracts);

            return Ok(contractsToReturn);
        }

        // GET: api/projectContract/5
        [HttpGet("{contractId}")]
        public async Task<IActionResult> Get(long contractId)
        {
            var contract = await _contract.FindById(contractId);

            if (contract == null)
                return NotFound();

            var contractToReturn = _mapper.Map<ProjectContractForListDTO>(contract);
            return Ok(contractToReturn);
        }


        // GET: api/projectContract/project/{projectId}
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> FindByProject(long projectId)
        {
            var contracts = await _contract.FindByProject(projectId);
            if (contracts == null)
                return NotFound();

            var contractsToReturn = _mapper.Map<List<ProjectContractForListDTO>>(contracts);

            return Ok(contractsToReturn);
        }

        // GET: api/projectContract/project/{projectId}
        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> FindByInternalSigner(long employeeId)
        {
            var contracts = await _contract.FindByInternalSigner(employeeId);
            if (contracts == null)
                return NotFound();

            var contractsToReturn = _mapper.Map<List<ProjectContractForListDTO>>(contracts);

            return Ok(contractsToReturn);
        }

        // POST: api/ProjectContract
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectContractForInsertDTO contract)
        {
            var contractToInsert = _mapper.Map<ProjectContract>(contract);

            if (!await _contract.Insert(contractToInsert))
                return BadRequest();

            return StatusCode(201);
        }

    }
}
