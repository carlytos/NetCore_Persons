using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProyect.Entities
{
    public class PersonsCollectivesContext :DbContext
    {
        public PersonsCollectivesContext(DbContextOptions<PersonsCollectivesContext> options)
            : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Collective> Collectives { get; set; }
    }
}
