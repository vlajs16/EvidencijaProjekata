using DAL;
using Domain;
using Logics.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public class ProjectProposalLogic : IProjectProposal
    {
        private readonly IRCContext _context;

        public ProjectProposalLogic(IRCContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectProposal>> FindObjects(string criteria)
        {
            try
            {
                criteria = criteria.ToLower();
                var projectsFromRepo = await _context.ProjectProposals
                    .Include(x => x.Company)
                    .Where(x=> x.Name.ToLower().Contains(criteria))
                    .ToListAsync();
                return projectsFromRepo;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<ProjectProposal> GetById(long projectProposalId)
        {
            try
            {
                var projectFromRepo = await _context.ProjectProposals
                    .Include(x => x.Company)
                    .Include(x => x.ExternalMentor)
                    .Include(x => x.Subjects).ThenInclude(p => p.ScientificArea)
                    .FirstOrDefaultAsync(x => x.ProjectProposalID == projectProposalId);
                return projectFromRepo;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<List<ProjectProposal>> GetObjects()
        {
            try
            {
                var projectsFromRepo = await _context.ProjectProposals
                    .Include(x => x.Company)
                    .ToListAsync();
                return projectsFromRepo;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Insert(ProjectProposal projectProposal)
        {
            try
            {
                var company = await _context.Companies
                    .FirstOrDefaultAsync(x => x.CompanyID == projectProposal.Company.CompanyID);
                if (company == null)
                    return false;
                projectProposal.Company = company;

                if(projectProposal.ExternalMentor.MentorID != 0)
                {
                    var externalMentor = await _context.ExternalMentors.Include(c => c.Contacts)
                        .Where(x => x.CompanyID == projectProposal.ExternalMentor.CompanyID
                        && x.MentorID == projectProposal.ExternalMentor.MentorID).FirstOrDefaultAsync();
                    var exMen = projectProposal.ExternalMentor;
                    if (externalMentor != null)
                        projectProposal.ExternalMentor = externalMentor;

                    foreach (var contact in exMen.Contacts)
                    {
                        if (contact.SerialNumber == 0)
                            projectProposal.ExternalMentor.Contacts.Add(contact);
                    }
                }

                foreach (var subj in projectProposal.Subjects)
                {
                    var pom = await _context.ScientificAreas
                        .FirstOrDefaultAsync(x => x.ScientificAreaID == subj.ScientificArea.ScientificAreaID);
                    if (pom == null)
                        return false;
                    subj.ScientificArea = pom;
                }

                await _context.ProjectProposals.AddAsync(projectProposal);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return false;
            }
        }
    }
}
