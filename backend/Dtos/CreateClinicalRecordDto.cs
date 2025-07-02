using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentalClinicApi.Dtos
{
    public class CreateClinicalRecordDto
    {
        [Required]
        public string AppointmentId { get; set; } = null!;

        [Required]
        public string PatientId { get; set; } = null!;

        [Required]
        public string DentistId { get; set; } = null!;

        [Required]
        [MinLength(3)]
        public string Diagnosis { get; set; } = null!;

        [Required]
        [MinLength(3)]
        public string Treatment { get; set; } = null!;

        public string Notes { get; set; } = string.Empty;
    }
}