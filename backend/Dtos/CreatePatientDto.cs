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
        public string Phone { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = null!;
    }
}