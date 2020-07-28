using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.EmployeePositionDTOs;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/employeePosition")]
    [ApiController]
    public class EmployeePositionController : ControllerBase
    {
        private IEmployeePositionLogic _employeePositionLogic;
        private readonly IMapper _mapper;
        public EmployeePositionController(IEmployeePositionLogic employeePositionLogic, IMapper mapper)
        {
            _employeePositionLogic = employeePositionLogic;
            _mapper = mapper;
        }
        // GET: api/EmployeePosition
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var empPos = await _employeePositionLogic.GetObjects();
            List<EmployeePositionDTO> empPosReturn = _mapper.Map<List<EmployeePositionDTO>>(empPos);
            return Ok(empPosReturn);
        }

        // GET: api/EmployeePosition/2
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> Get(int employeeId)
        {
            var empPos = await _employeePositionLogic.GetById(employeeId);
            List<EmployeePositionDTO> empPosReturn = _mapper.Map<List<EmployeePositionDTO>>(empPos);
            return Ok(empPosReturn);
        }

        // GET: api/EmployeePosition/5
        [HttpGet("{employeeId}/{positionId}")]
        public async Task<IActionResult> Get(int employeeId, int positionId)
        {
            var ep = await _employeePositionLogic.Find(employeeId, positionId);
            if (ep == null)
                return NotFound($"Ne postoji zaposleni sa rednim brojem:{employeeId} koji je radio na poziciji sa rednim brojem:{positionId}");
            EmployeePositionDTO empPosReturn = _mapper.Map<EmployeePositionDTO>(ep);
            return Ok(empPosReturn);
        }
    }
}
