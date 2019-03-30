using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProyect.Models
{
    public class CollectiveDto
    {     
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Entity { get; set; }
    }
}
