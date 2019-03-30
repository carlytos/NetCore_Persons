using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProyect.Entities
{
    public static class PersonCollectivesData
    {
        public static void EnsureSeedDataForContext(this PersonsCollectivesContext context)
        {
            // first, clear the database.  This ensures we can always start 
            // fresh with each demo.  Not advised for production environments, obviously :-)

            context.Persons.RemoveRange(context.Persons);
            context.SaveChanges();


            //init seed data
            var persons = new List<Person>()
            {
                new Person()
                {
                    Id = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                    Name = "Carlos",
                    Surname = "Del Río",
                    Email = "carlos@barriolapinada.es",
                    DateOfBirth = new DateTimeOffset(new DateTime(1994,9,30))
                },
                
                new Person()
                {
                    Id = new Guid("76053df4-6687-4353-8937-b45556748abe"),
                    Name = "Victor",
                    Surname = "Doe",
                    Email = "victor@barriolapinada.es",
                    DateOfBirth = new DateTimeOffset(new DateTime(1970,2,2))
                    
                }
            };

            var collectives = new List<Collective>()
            {   
                new Collective()
                {
                    Id = new Guid("e7befcc7-1be0-43d3-a728-1442867b6b09"),
                    Name = "La Pinada",
                    Description = "Eco barrio",
                    Entity = "Zuby labs",
                }
            };

            context.Persons.AddRange(persons);
            context.Collectives.AddRange(collectives);
            context.SaveChanges();
        }

    }
}
