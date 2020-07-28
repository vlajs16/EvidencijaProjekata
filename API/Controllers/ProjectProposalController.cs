using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.ProjectProposalDTOs;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Treba dodati da samo kompanija može da unosi ako je ulogovana...
    [Route("api/projectProposal")]
    [ApiController]
    public class ProjectProposalController : ControllerBase
    {
        private readonly IProjectProposal _pojectLogic;
        private readonly IMapper _mapper;

        public ProjectProposalController(IProjectProposal pojectLogic, IMapper mapper)
        {
            _pojectLogic = pojectLogic;
            _mapper = mapper;
        }
        // GET: api/projectProposal
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projectsFromRepo = await _pojectLogic.GetObjects();
            if (projectsFromRepo == null)
                return NotFound();

            var projectsToReturn = _mapper.Map<List<ProjectProposalForListDTO>>(projectsFromRepo);

            return Ok(projectsToReturn);
        }

        // GET: api/projectProposal/{projectProposalId}
        [HttpGet("{projectProposalId}")]
        public async Task<IActionResult> Get(int projectProposalId)
        {
            var projectFromRepo = await _pojectLogic.GetById(projectProposalId);
            if (projectFromRepo == null)
                return NotFound();

            var projectToReturn = _mapper.Map<ProjectProposalDetailsDTO>(projectFromRepo);
            return Ok(projectToReturn);
        }

        // GET: api/projectProposal/criteria/{criteria}
        [HttpGet("criteria/{criteria}")]
        public async Task<IActionResult> Get(string criteria)
        {
            var projectsFromRepo = await _pojectLogic.FindObjects(criteria);
            if (projectsFromRepo == null)
                return NotFound();

            var projectsToReturn = _mapper.Map<List<ProjectProposalForListDTO>>(projectsFromRepo);

            return Ok(projectsToReturn);
        }

        // POST: api/projectProposal
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectProposalForInsertDTO project)
        {
            var projectToInsert = _mapper.Map<ProjectProposal>(project);
            projectToInsert.ExternalMentor.CompanyID = projectToInsert.Company.CompanyID;


            if (!await _pojectLogic.Insert(projectToInsert))
                return BadRequest();

            return Ok();
        }
    }
}
