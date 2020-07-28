using DAL;
using Domain;
using Logics.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private IRCContext _context;
        public EmployeeLogic(IRCContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetObjects()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> Login(string username, string password)
        {
            var user = await _context.Employees.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
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

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Employees.AnyAsync(x => x.Username == username))
                return true;
            return false;
        }

        public async Task<Employee> Register(Employee employee, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            employee.PasswordHash = passwordHash;
            employee.PasswordSalt = passwordSalt;

            //await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<Employee> GetObjectByUsername(string username)
        {
            if (!await UserExists(username))
                return null;
            return await _context.Employees.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<List<Employee>> GetObjectByName(string value)
        {
            return await _context.Employees
                .Where(x => x.Name.Contains(value) || x.Surname.Contains(value))
                .ToListAsync();
        }
        public async Task<Employee> GetByID(long employeeId)
        {
            try
            {
                return await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeID == employeeId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>> " + ex.Message);
                return null;
            }
        }
    }
}
