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
    public class ExternalMentorContactLogic : IExternalMentorContactLogic
    {
        private readonly IRCContext _context;
        public ExternalMentorContactLogic(IRCContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(int companyId, int mentorId, int serialNumber)
        {
            ExternalMentorContact contact = await _context.ExternalMentorContacts
                .FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
            if (contact == null)
                return false;
            _context.ExternalMentorContacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ExternalMentorContact>> Find(int mentorId, string value)
        {
            return await _context.ExternalMentorContacts
                .Where(x => x.ExternalMentorMentorID == mentorId && x.Value.Contains(value))
                .ToListAsync();
        }

        public async Task<List<ExternalMentorContact>> GetObjectsForMentor(int mentorId)
        {
            return await _context.ExternalMentorContacts
                .Where(x => x.ExternalMentorMentorID == mentorId)
                .ToListAsync();
        }

        public async Task<bool> Insert(ExternalMentorContact contact)
        {
            try
            {
                _context.ExternalMentorContacts.Add(contact);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>" + ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(ExternalMentorContact externalMentorContact)
        {
            ExternalMentorContact contact = await _context.ExternalMentorContacts
                .FirstOrDefaultAsync(x => x.SerialNumber == externalMentorContact.SerialNumber);
            if (contact == null)
            {
                return false;
            }
            contact.ContactType = externalMentorContact.ContactType;
            contact.Value = externalMentorContact.Value;
            _context.ExternalMentorContacts.Update(contact);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
