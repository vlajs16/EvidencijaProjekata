using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface IProjectContractLogic
    {
        Task<List<ProjectContract>> GetObjects();
        Task<List<ProjectContract>> FindByProject(long projectId);
        Task<List<ProjectContract>> FindByInternalSigner(long employeeId);
        Task<ProjectContract> FindById(long projectContractId);
        Task<bool> Insert(ProjectContract projectContract);
        Task<bool> Update(ProjectContract projectContract);
    }
}
