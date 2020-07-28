using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface IEmployeePositionLogic
    {
        Task<List<EmployeePosition>> GetObjects();
        Task<List<EmployeePosition>> GetById(int employeeId);
        Task<EmployeePosition> Find(int employeeId,int positionId);
    }
}
