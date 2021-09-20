using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHIProject.Data;
using Microsoft.EntityFrameworkCore;

namespace EHIProject.Model
{
    public class SQLContactRepository : IContactRepository
    {
        private readonly DatabaseContex _contex;
        public SQLContactRepository(DatabaseContex contex)
        {
            _contex = contex;
        }

        public async Task<IEnumerable<Contact>> GetContact()
        {
            return await _contex.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContactById(int id)
        {
            return await _contex.Contacts.FindAsync(id);
        }

        public async Task<Contact> Add(Contact contact)
        {
            contact.Status = true;
            await _contex.Contacts.AddAsync(contact);
            await _contex.SaveChangesAsync();
            return contact;
        }

        public Contact Update(Contact Contact)
        {
            var changesContact = _contex.Contacts.Attach(Contact);
            changesContact.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contex.SaveChanges();
            return Contact;
        }

        public Contact Delete(int id)
        {
            Contact contact = GetContactById(id).Result;
            contact.Status = false;

            var changesContact = _contex.Contacts.Attach(contact);
            changesContact.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contex.SaveChanges();
            return contact;
        }
    }
}
