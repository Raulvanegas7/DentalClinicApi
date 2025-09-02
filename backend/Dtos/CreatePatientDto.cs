using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Dtos
{
    public class CreatePatientDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        
        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        public string? Address { get; set; }

    }
}