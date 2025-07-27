using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos
{
    public class UpdatePatientDto
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = null!;

        public DateTime? BirthDate { get; set; }
    }
}