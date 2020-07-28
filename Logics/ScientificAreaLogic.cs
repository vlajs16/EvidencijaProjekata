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
    public class ScientificAreaLogic : IScientificArea
    {
        private readonly IRCContext _context;

        public ScientificAreaLogic(IRCContext context)
        {
            _context = context;
        }

        public async Task<List<ScientificArea>> Find(string criteria)
        {
            try
            {
                var scientificAreas = await _context.ScientificAreas
                    .Where(x => x.Name.ToLower().Contains(criteria.ToLower())).ToListAsync();
                return scientificAreas;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<ScientificArea> GetById(long scientificAreaId)
        {
            try
            {
                var scientificArea = await _context.ScientificAreas
                    .FirstOrDefaultAsync(x => x.ScientificAreaID == scientificAreaId);
                return scientificArea;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<List<ScientificArea>> GetObjects()
        {
            try
            {
                var scientificAreas = await _context.ScientificAreas.ToListAsync();
                return scientificAreas;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>" + ex.Message);
                return null;
            }
        }
    }
}
