using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProyect.Entities;

namespace FinalProyect.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons();
        Person GetPerson(Guid personId);
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
        bool Save();
    }
}
