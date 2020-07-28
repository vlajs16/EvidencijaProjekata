using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface ICompanyLogic
    {
        Task<List<Company>> GetObjects();
        Task<List<Company>> FindObjects(string criteria);
        Task<Company> GetByID(long id);
        Task<bool> Update(Company company);
        Task<bool> Insert(Company company, string password);
    }
}
