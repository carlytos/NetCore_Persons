using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProyect.Entities;

namespace FinalProyect.Repositories
{
    public interface ICollectiveRepository
    {
        IEnumerable<Collective> GetCollectives();
        Collective GetCollective(Guid collectiveId);
        void AddCollective(Collective collective);
        void UpdateCollective(Collective collective);
        void DeleteCollective(Collective collective);
        bool Save();

    }
}
