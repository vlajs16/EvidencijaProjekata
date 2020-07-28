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
    public class ExternalMentorLogic : IExternalMentorLogic
    {
        private readonly IRCContext _context;
        public ExternalMentorLogic(IRCContext context)
        {
            _context = context;
        }

        public async Task<List<ExternalMentor>> GetObjects()
        {
            var mentors = await _context.ExternalMentors
                .Include(x => x.Contacts)
                .ToListAsync();
            return mentors;
        }
        public async Task<ExternalMentor> GetByID(int id)
        {
            return await _context.ExternalMentors
                .Include(x => x.Contacts)
                .FirstOrDefaultAsync(x => x.MentorID == id);
        }

        public async Task<List<ExternalMentor>> Find(string value)
        {
            return await _context.ExternalMentors
                .Include(x => x.Contacts)
                .Where(x => x.Name.Contains(value) || x.Surname.Contains(value))
                .ToListAsync();
        }

        public async Task<bool> Insert(ExternalMentor mentor)
        {
            try
            {
                _context.ExternalMentors.Add(mentor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>" + ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(ExternalMentor mentor)
        {
            ExternalMentor m = await _context.ExternalMentors
                .FirstOrDefaultAsync(p => p == mentor);
            if (m == null)
                return false;
            m.Name = mentor.Name;
            m.Surname = mentor.Surname;
            //m.Contacts = mentor.Contacts;
            _context.ExternalMentors.Update(m);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(long companyId, int mentorId)
        {
            ExternalMentor mentor = await _context.ExternalMentors
                .FirstOrDefaultAsync(x => x.CompanyID == companyId && x.MentorID == mentorId);
            if (mentor == null)
                return false;
            _context.ExternalMentors.Remove(mentor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
