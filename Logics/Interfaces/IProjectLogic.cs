using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface IProjectLogic
    {
        Task<List<Project>> GetObjects();
        Task<List<Project>> FindObjects(string criteria);
        Task<Project> GetById(long projectId);
        Task<bool> Insert(Project project);
        Task<bool> Update(Project project);
        Task<bool> Delete(Project project);
        Task<int[]> GetNumbers();
    }
}
