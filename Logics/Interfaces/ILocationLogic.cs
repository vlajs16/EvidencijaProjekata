using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface ILocationLogic
    {
        Task<List<Location>> GetObjects(long companyId);
        Task<Location> GetById(long companyId, long cityId);
        Task<bool> Insert(Location location);
        Task<bool> Delete(long companyId, long cityId);
        Task<bool> Update(Location location);


    }
}
