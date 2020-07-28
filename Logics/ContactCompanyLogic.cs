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
    public class ContactCompanyLogic : IContactCompany
    {
        private readonly IRCContext _context;

        public ContactCompanyLogic(IRCContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(Contact contact)
        {
            try
            {
                var contactFromRepo = await _context.Contacts
                    .FirstOrDefaultAsync(x => x.CompanyID == contact.CompanyID && x.ContactID == contact.ContactID);
                if (contactFromRepo == null)
                    return false;
                _context.Contacts.Remove(contactFromRepo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>" + ex);
                return false;
            }
        }

        public async Task<Contact> GetByIds(long companyId, long contactId)
        {
            try
            {
                return await _context.Contacts
                    .FirstOrDefaultAsync(x => x.CompanyID == companyId && x.ContactID == contactId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return null;
                throw;
            }
        }

        public async Task<List<Contact>> GetObjects(long companyId)
        {
            return await _context.Contacts.Where(x => x.CompanyID == companyId).ToListAsync();
        }

        public async Task<bool> Insert(Contact contact)
        {
            try
            {
                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(Contact contact)
        {
            try
            {
                var contactFromRepo = await _context.Contacts
                    .FirstOrDefaultAsync(x => x.CompanyID == contact.CompanyID && x.ContactID == contact.ContactID);
                if (contactFromRepo == null)
                    return false;
                contactFromRepo.Value = contact.Value;
                contactFromRepo.ContactType = contact.ContactType;
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
