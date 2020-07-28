using DAL;
using Domain;
using Logics.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Logics
{
    public class PositionLogic : IPositionLogic
    {
        private IRCContext _context;
        public PositionLogic(IRCContext context)
        {
            _context = context;
        }

        public async Task<Position> Find(int id)
        {
            return await _context.Positions.FirstOrDefaultAsync(p => p.PositionID == id);
        }

        public async Task<List<Position>> FindByName(string value)
        {
            return await _context.Positions.Where(x => x.Name.Contains(value)).ToListAsync();
        }

        public async Task<List<Position>> GetObjects()
        {
            return await _context.Positions.ToListAsync();
        }

        public async Task<bool> Insert(Position position)
        {
            try
            {
                _context.Positions.Add(position);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>" + ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(Position position)
        {
            Position p = await _context.Positions.FirstOrDefaultAsync(x => x.PositionID == position.PositionID);
            if(p == null){
                return false;
            }
            p.Name = position.Name;
            _context.Positions.Update(p);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
