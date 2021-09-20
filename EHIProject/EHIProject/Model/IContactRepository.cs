using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHIProject.Data;

namespace EHIProject.Model
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetContact();
        Task<Contact> GetContactById(int id);
        Task<Contact> Add(Contact Contact);
        Contact Update(Contact Contact);
        Contact Delete(int id);
    }
}
