using DAL;
using Domain;
using Logics.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Logics
{
    public class CompanyLogic : ICompanyLogic
    {
        private readonly IRCContext _context;

        public CompanyLogic(IRCContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> FindObjects(string criteria)
        {
            try
            {
                return await _context.Companies
                    .Where(c =>  c.Name.Contains(criteria) || c.CompanyUsername.Contains(criteria))
                    .Include(x => x.Locations)
                    .Include(x => x.Mentors)
                    .Include(x => x.Contacts)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Company> GetByID(long id)
        {
            try
            {
                Company company = await _context.Companies
                    .Include(x => x.Locations)
                    .Include(x => x.Mentors).ThenInclude(c => c.Contacts)
                    .Include(x => x.Contacts).FirstOrDefaultAsync(c => c.CompanyID == id);
                return company;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Company>> GetObjects()
        {
            try
            {
                return await _context.Companies
                    .Include(x => x.Locations)
                    .Include(x => x.Mentors)
                    .Include(x => x.Contacts).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Insert(Company company, string password)
        {
            try
            {
                foreach (var location in company.Locations)
                {
                    location.City = await _context.Cities.FirstOrDefaultAsync(x => x.CityID == location.City.CityID);
                }
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                company.PasswordHash = passwordHash;
                company.PasswordSalt = passwordSalt;
                await _context.Companies.AddAsync(company);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>> " + ex.Message);
                return false;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> Update(Company company)
        {
            try
            {
                Company companyToChange = await _context.Companies.FirstOrDefaultAsync(c => c.CompanyID == company.CompanyID);
                if (companyToChange == null)
                    return false;
                companyToChange.Name = company.Name;
                _context.Companies.Update(companyToChange);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
