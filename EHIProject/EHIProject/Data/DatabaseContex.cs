using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHIProject.Data
{
    public class DatabaseContex : DbContext
    {
        public DatabaseContex(DbContextOptions option) : base(option)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
