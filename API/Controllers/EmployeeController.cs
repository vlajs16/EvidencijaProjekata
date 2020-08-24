using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects;
using Domain;
using Logics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeLogic _logic;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeLogic logic, IConfiguration config, IMapper mapper)
        {
            _logic = logic;
            _config = config;
            _mapper = mapper;
        }
        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _logic.GetObjects();
            List<EmployeeDTO> employeesReturn = _mapper.Map<List<EmployeeDTO>>(employees);
            return Ok(employeesReturn);
        }

        // GET: api/Employee/tea
        [HttpGet("{value}")]
        public async Task<IActionResult> Get(string value)
        {
            var employees = await _logic.GetObjectByName(value);
            List<EmployeeDTO> employeeReturn = _mapper.Map<List<EmployeeDTO>>(employees);
            return Ok(employeeReturn);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(EmployeeForLogin user)
        {
            user.Username = user.Username.ToLower();

            //if (await _logic.UserExists(user.Username))
            //    return BadRequest("Username already exists!");

            var userForRegistration = await _logic.GetObjectByUsername(user.Username);

            if (userForRegistration == null)
                return BadRequest($"Employee with username:'{user.Username}' doesn't exist.");

            var createdUser = await _logic.Register(userForRegistration, user.Password);

            return StatusCode(201);
        }
        // GET: api/Employee/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(EmployeeForLogin userForLogin)
        {
            var userFromRepo = await _logic.Login(userForLogin.Username.ToLower(), userForLogin.Password.ToLower());
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, userFromRepo.EmployeeID.ToString()),
               new Claim(ClaimTypes.Name, userFromRepo.Username),
               new Claim(ClaimTypes.Actor, "employee")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_config.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var empToReturn = _mapper.Map<EmployeeDTO>(userFromRepo);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user = empToReturn
            });
        }
    }
}
