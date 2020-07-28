using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface IContactCompany
    {
        Task<List<Contact>> GetObjects(long companyId);
        Task<Contact> GetByIds(long companyId, long contactId);
        Task<bool> Insert(Contact contact);
        Task<bool> Update(Contact contact);
        Task<bool> Delete(Contact contact);
    }
}
