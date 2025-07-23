using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos
{
    public class UpdatePatientDto
    {
        public string Name { get; set; } 

        public string Email { get; set; }

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}