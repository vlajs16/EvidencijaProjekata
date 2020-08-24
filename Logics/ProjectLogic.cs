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
    public class ProjectLogic : IProjectLogic
    {
        private readonly IRCContext _context;

        public ProjectLogic(IRCContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(Project project)
        {
            try
            {
                var projectForDelete = await _context.Projects
                    .FirstOrDefaultAsync(x => x.ProjectID == project.ProjectID);
                if (projectForDelete == null)
                    return false;
                _context.Projects.Remove(projectForDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<List<Project>> FindObjects(string criteria)
        {
            try
            {
                var project = await _context.Projects
                    .Include(x => x.InternalMentor)
                    .Include(x => x.DecisionMaker)
                    .Include(x => x.ProjectProposal)
                        .ThenInclude(x => x.Company)
                    .Include(x => x.ProjectProposal)
                        .ThenInclude(x => x.ExternalMentor)
                    .Include(x => x.ProjectProposal)
                        .ThenInclude(x => x.Subjects).ThenInclude(p => p.ScientificArea)
                    .Where(x => x.ProjectProposal.Name.ToLower().Contains(criteria.ToLower())
                                || x.ProjectProposal.Description.ToLower().Contains(criteria.ToLower()))
                    .ToListAsync();
                return project;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<Project> GetById(long projectId)
        {
            try
            {
                var project = await _context.Projects
                    .Include(x => x.InternalMentor)
                    .Include(x => x.DecisionMaker)
                    .Include(x => x.ProjectProposal)
                        .ThenInclude(x => x.Company)
                    .Include(x => x.ProjectProposal)
                        .ThenInclude(x => x.ExternalMentor)
                    .Include(x => x.ProjectProposal)
                        .ThenInclude(x => x.Subjects).ThenInclude(p => p.ScientificArea)
                    .FirstOrDefaultAsync(x => x.ProjectID == projectId);
                return project;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<int[]> GetNumbers()
        {
            try
            {
                int approved = await _context.Projects.CountAsync();
                int notApproved = await _context.ProjectProposals.Where(x => x.Approved == false).CountAsync();
                return new int[] { approved, notApproved };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<List<Project>> GetObjects()
        {
            try
            {
                var projects = await _context.Projects
                    .Include(x => x.InternalMentor)
                    .Include(x => x.DecisionMaker)
                    .Include(x => x.ProjectProposal)
                        .ThenInclude(x => x.Company)
                    .Include(x => x.ProjectProposal)
                        .ThenInclude(x => x.ExternalMentor)
                    .Include(x => x.ProjectProposal)
                        .ThenInclude(x => x.Subjects).ThenInclude(p => p.ScientificArea)
                    .ToListAsync();
                return projects;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Insert(Project project)
        {
            try
            {
                project.InternalMentor = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeID == project.InternalMentor.EmployeeID);
                project.ProjectProposal = await _context.ProjectProposals.FirstOrDefaultAsync(x => x.ProjectProposalID == project.ProjectProposal.ProjectProposalID);
                project.DecisionMaker = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeID == project.DecisionMaker.EmployeeID);
                if (project.InternalMentor == null || project.ProjectProposal == null || project.DecisionMaker == null)
                    return false;

                project.AdoptionDate = DateTime.Now;
                project.ProjectProposal.Approved = true;

                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(Project project)
        {
            try
            {
                var projectForUpdate = await _context.Projects
                       .FirstOrDefaultAsync(x => x.ProjectID == project.ProjectID);
                if (projectForUpdate == null)
                    return false;
                projectForUpdate.InternalMentor = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeID == project.InternalMentor.EmployeeID);
                projectForUpdate.ProjectProposal = await _context.ProjectProposals.FirstOrDefaultAsync(x => x.ProjectProposalID == project.ProjectProposal.ProjectProposalID);
                projectForUpdate.DecisionMaker = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeID == project.DecisionMaker.EmployeeID);
                if (project.InternalMentor == null || project.ProjectProposal == null || project.DecisionMaker == null)
                    return false;
                projectForUpdate.Note = project.Note;
                projectForUpdate.AdoptionDate = project.AdoptionDate;
                projectForUpdate.Description = project.Description;
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
