using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface IExternalMentorContactLogic
    {
        Task<List<ExternalMentorContact>> GetObjectsForMentor(int mentorId);
        Task<List<ExternalMentorContact>> Find(int mentorId, string value);
        Task<bool> Update(ExternalMentorContact contact);
        Task<bool> Delete(int companyId, int mentorId, int serialNumber);
        Task<bool> Insert(ExternalMentorContact contact);
    }
}
