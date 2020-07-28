using DAL;
using Domain;
using Logics.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public class EmployeePositionLogic : IEmployeePositionLogic
    {
        private IRCContext _context;
        public EmployeePositionLogic(IRCContext context)
        {
            _context = context;
        }
        public async Task<EmployeePosition> Find(int employeeId, int positionId)
        {
            return await _context.EmployeePositions
                .Include(emp => emp.Employee)
                .Include(pos => pos.Position)
                .FirstOrDefaultAsync();
                 
        }

        public async Task<List<EmployeePosition>> GetById(int employeeId)
        {
            return await _context.EmployeePositions
                .Include(emp => emp.Employee)
                .Include(pos => pos.Position)
                .Where(x => x.EmployeeID == employeeId)
                .ToListAsync();
        }

        public async Task<List<EmployeePosition>> GetObjects()
        {
            return await _context.EmployeePositions
                .Include(emp => emp.Employee)
                .Include(pos => pos.Position)
                .ToListAsync();
        }
    }
}
