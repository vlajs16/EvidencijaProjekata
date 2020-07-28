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
    public class ProjectContractLogic : IProjectContractLogic
    {
        private readonly IRCContext _context;

        public ProjectContractLogic(IRCContext context)
        {
            _context = context;
        }

        public async Task<ProjectContract> FindById(long projectContractId)
        {
            try
            {
                var contract = await _context.ProjectContracts
                    .Include(x => x.Project).ThenInclude(x => x.ProjectProposal)
                    .ThenInclude(x => x.Company)
                    .Include(x => x.InternalSigner)
                    .FirstOrDefaultAsync(x => x.ProjectContractID == projectContractId);
                return contract;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<List<ProjectContract>> FindByInternalSigner(long employeeId)
        {
            try
            {
                var contracts = await _context.ProjectContracts
                    .Include(x => x.Project).Include(x => x.InternalSigner)
                    .Where(x => x.InternalSigner.EmployeeID == employeeId)
                    .ToListAsync();
                return contracts;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<List<ProjectContract>> FindByProject(long projectId)
        {
            try
            {
                var contracts = await _context.ProjectContracts
                    .Include(x => x.Project)
                    .Include(x => x.InternalSigner)
                    .Where(x => x.Project.ProjectID == projectId)
                    .ToListAsync();
                return contracts;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<List<ProjectContract>> GetObjects()
        {
            try
            {
                var contracts = await _context.ProjectContracts
                    .Include(x => x.Project).Include(x => x.InternalSigner).ToListAsync();
                return contracts;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Insert(ProjectContract projectContract)
        {
            try
            {
                var project = await _context.Projects
                    .FirstOrDefaultAsync(x => x.ProjectID == projectContract.Project.ProjectID);
                if (project == null)
                    return false;
                projectContract.Project = project;

                var employee = await _context.Employees
                    .FirstOrDefaultAsync(x => x.EmployeeID == projectContract.InternalSigner.EmployeeID);
                if (employee == null)
                    return false;
                projectContract.InternalSigner = employee;

                if (projectContract.SigningDate == null ||
                    string.IsNullOrWhiteSpace(projectContract.CompanySigner)
                    || projectContract.ExpiryDate == null)
                    return false;

                await _context.ProjectContracts.AddAsync(projectContract);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>> " + ex.Message);
                return false;
            }
        }

        public Task<bool> Update(ProjectContract projectContract)
        {
            throw new NotImplementedException();
        }
    }
}
