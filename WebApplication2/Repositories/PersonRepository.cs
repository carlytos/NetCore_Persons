using FinalProyect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FinalProyect.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private PersonsCollectivesContext _context;

        public PersonRepository(PersonsCollectivesContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetPersons()
        {
            return _context.Persons
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Surname)
                .ToList();
        }

        public Person GetPerson(Guid personId)
        {
            return _context.Persons.FirstOrDefault(p => p.Id == personId);
        }

        public void AddPerson(Person person)
        {
            person.Id = Guid.NewGuid();
            _context.Persons.Add(person);

        }

        public void UpdatePerson(Person person)
        {
            _context.Persons.Update(person);
        }

        public void DeletePerson(Person person)
        {
            _context.Persons.Remove(person);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }


    }
}
