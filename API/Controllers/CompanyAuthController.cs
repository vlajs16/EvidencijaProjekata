using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.CompanyDTOs;
using Logics.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/companyAuth")]
    [ApiController]
    public class CompanyAuthController : ControllerBase
    {
        private readonly ICompanyAuthLogic _companyAuth;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ICompanyLogic _companyLogic;

        public CompanyAuthController(ICompanyAuthLogic companyAuth, IMapper mapper, IConfiguration config, ICompanyLogic companyLogic)
        {
            _companyAuth = companyAuth;
            _mapper = mapper;
            _config = config;
            _companyLogic = companyLogic;
        }

        // POST: api/CompanyAuth
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] CompanyToLoginDTO companyToLogin)
        {
            companyToLogin.Username = companyToLogin.Username.ToLower();

            var companyFromRepo = await _companyAuth.Login(companyToLogin.Username, companyToLogin.Password);
            if (companyFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, companyFromRepo.CompanyID.ToString()),
                new Claim(ClaimTypes.Name, companyFromRepo.CompanyUsername),
                new Claim(ClaimTypes.Actor, "company")
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

            var companyToReturn = _mapper.Map<CompanyForListDTO>(companyFromRepo);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                company = companyToReturn
            });

        }
        [Authorize]
        [HttpPost("password/{companyId}")]
        public async Task<IActionResult> ChangePassword(long companyId, [FromBody] CompanyChangePasswordDTO company)
        {
            if (companyId != long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("You are not logged in");

            var companyFromRepo = await _companyLogic.GetByID(companyId);

            if (companyFromRepo.CompanyUsername != User.FindFirst(ClaimTypes.Name).Value)
                return Unauthorized("You are not logged in");
                
            if (!await _companyAuth.ChangePassword(companyFromRepo.CompanyUsername, company.Password))
                return BadRequest();
            return Ok();
        }
    }
}
