using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface IEmployeeLogic
    {
        Task<List<Employee>> GetObjects();
        Task<bool> UserExists(string username);
        Task<Employee> Register(Employee employee, string password);
        Task<Employee> Login(string username, string password);
        Task<Employee> GetObjectByUsername(string username);
        Task<List<Employee>> GetObjectByName(string value);
        Task<Employee> GetByID(long employeeId);
    }
}
