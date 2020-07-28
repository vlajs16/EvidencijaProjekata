using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface IProjectProposal
    {
        Task<List<ProjectProposal>> GetObjects();
        Task<List<ProjectProposal>> FindObjects(string criteria);
        Task<ProjectProposal> GetById(long projectProposalId);
        Task<bool> Insert(ProjectProposal projectProposal);
    }
}
