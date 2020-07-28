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
    public class LocationLogic : ILocationLogic
    {
        private readonly IRCContext _context;

        public LocationLogic(IRCContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(long companyId, long cityId)
        {
            try
            {
                var locationToRemove = await _context.Locations
                    .FirstOrDefaultAsync(x => x.CompanyID == companyId && x.CityID == cityId);
                if (locationToRemove == null)
                    return false;
                _context.Locations.Remove(locationToRemove);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<Location> GetById(long companyId, long cityId)
        {
            try
            {
                var locationFromRepo = 
                    await _context.Locations.Include(x => x.City).FirstOrDefaultAsync(x => x.CompanyID == companyId && x.CityID == cityId);
                return locationFromRepo;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<List<Location>> GetObjects(long companyId)
        {
            try
            {
                var locations = await _context.Locations
                    .Where(x => x.CompanyID == companyId)
                    .Include(x => x.City)
                    .ToListAsync();
                return locations;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Insert(Location location)
        {
            try
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.CityID == location.City.CityID);
                if (city == null)
                    return false;
                location.City = city;
                _context.Locations.Add(location);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(Location location)
        {
            try
            {
                var locationFromRepo = await _context.Locations
                    .FirstOrDefaultAsync(x => x.CompanyID == location.CompanyID && x.CityID == location.CityID);
                if (locationFromRepo == null)
                    return false;
                locationFromRepo.StreetName = location.StreetName;
                locationFromRepo.StreetNumber = location.StreetNumber;
                locationFromRepo.Floor = location.Floor;
                locationFromRepo.AppartmentNumber = location.AppartmentNumber;
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
