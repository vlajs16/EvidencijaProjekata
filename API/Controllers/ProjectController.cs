using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.ProjectDTOs;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectLogic _logic;
        private readonly IMapper _mapper;

        public ProjectController(IProjectLogic pojectLogic, IMapper mapper)
        {
            _logic = pojectLogic;
            _mapper = mapper;
        }
        // GET: api/Project
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = await _logic.GetObjects();
            if (projects == null)
                return NotFound();
            var projectsToReturn = _mapper.Map<List<ProjectsForListDTO>>(projects);
            return Ok(projectsToReturn);
        }

        // GET: api/Project/5
        [HttpGet("{projectId}", Name = "Get")]
        public async Task<IActionResult> Get(int projectId)
        {
            var project = await _logic.GetById(projectId);
            if (project == null)
                return NotFound();
            var projectToReturn = _mapper.Map<ProjectDetailsDTO>(project);
            return Ok(projectToReturn);
        }

        // GET: api/project/criteria/{value}
        [HttpGet("criteria/{value}")]
        public async Task<IActionResult> Get(string value)
        {
            var projects = await _logic.FindObjects(value);
            if (projects == null)
                return NotFound();
            var projectsToReturn = _mapper.Map<List<ProjectsForListDTO>>(projects);
            return Ok(projectsToReturn);
        }

        // POST: api/Project
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectForInsertDTO projectForInsert)
        {
            var project = _mapper.Map<Project>(projectForInsert);
            if (!await _logic.Insert(project))
                return BadRequest();
            return Ok();
        }

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProjectForInsertDTO project)
        {
            var projectForUpdate = _mapper.Map<Project>(project);
            projectForUpdate.ProjectID = id;
            if (!await _logic.Update(projectForUpdate))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var projectForDelete = await _logic.GetById(id);
            if (projectForDelete == null)
                return NotFound();
            if (!await _logic.Delete(projectForDelete))
                return BadRequest();
            return Ok();
        }
    }
}
