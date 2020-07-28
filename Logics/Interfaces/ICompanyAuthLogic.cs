using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface ICompanyAuthLogic
    {
        Task<Company> Login(string companyUsername, string password);
        Task<bool> ChangePassword(string companyUsername, string newPassword);
    }
}
