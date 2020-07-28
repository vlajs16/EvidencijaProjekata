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
    public class CityLogic : ICityLogic
    {
        private readonly IRCContext _context;

        public CityLogic(IRCContext context)
        {
            _context = context;
        }

        public async Task<List<City>> Find(string criteria)
        {
            return await _context.Cities
                .Where(x => x.Name.Contains(criteria) || x.PostalCode.Contains(criteria))
                .ToListAsync();
        }

        public async Task<List<City>> GetAll()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> GetById(float id)
        {
            return await _context.Cities.FirstOrDefaultAsync(x => x.CityID == id);
        }
    }
}
