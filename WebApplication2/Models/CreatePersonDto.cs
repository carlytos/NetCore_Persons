using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProyect.Models
{
    public class CreatePersonDto
    {  
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname{ get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }

    }
}
