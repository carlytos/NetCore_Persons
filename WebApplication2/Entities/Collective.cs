using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProyect.Entities
{
    public class Collective
    {

        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        public string Entity { get; set; }
        /*[Required]
        public ICollection<Person> Members { get; set; }
            = new List<Person>();*/

    }
}
