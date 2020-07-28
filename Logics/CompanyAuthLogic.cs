using DAL;
using Domain;
using Logics.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public class CompanyAuthLogic : ICompanyAuthLogic
    {
        private readonly IRCContext _context;

        public CompanyAuthLogic(IRCContext context)
        {
            _context = context;
        }
        public async Task<bool> ChangePassword(string companyUsername, string newPassword)
        {
            try
            {
                var company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyUsername == companyUsername);
                if (company == null)
                    return false;
                byte[] passwordHash, passwordSalt;

                CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
                company.PasswordHash = passwordHash;
                company.PasswordSalt = passwordSalt;

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

        public async Task<Company> Login(string companyUsername, string password)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyUsername == companyUsername);

            if (company == null)
                return null;

            if (!VerifyPasswordHash(password, company.PasswordHash, company.PasswordSalt))
                return null;

            return company;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }
    }
}
