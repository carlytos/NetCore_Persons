using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProyect.Entities;

namespace FinalProyect.Repositories
{
    public class CollectiveRepository : ICollectiveRepository
    {
        private PersonsCollectivesContext _context;

        public CollectiveRepository(PersonsCollectivesContext context)
        {
            _context = context;
        }
        public IEnumerable<Collective> GetCollectives()
        {
            return _context.Collectives
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Description)
                .ToList();
        }

        public Collective GetCollective(Guid collectiveId)
        {
            return _context.Collectives.FirstOrDefault(c => c.Id == collectiveId);
        }

        public void AddCollective(Collective collective)
        {
            collective.Id = Guid.NewGuid();
            _context.Collectives.Add(collective);
        }

        public void UpdateCollective(Collective collective)
        {
            _context.Collectives.Update(collective);
        }

        public void DeleteCollective(Collective collective)
        {
            _context.Collectives.Remove(collective);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
