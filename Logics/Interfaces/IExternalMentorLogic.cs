using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface IExternalMentorLogic
    {
        Task<List<ExternalMentor>> GetObjects();
        Task<ExternalMentor> GetByID(int id);
        Task<List<ExternalMentor>> Find(string value);
        Task<bool> Update(ExternalMentor mentor);
        Task<bool> Delete(long companyId, int mentorId);
        Task<bool> Insert(ExternalMentor mentor);
    }
}
