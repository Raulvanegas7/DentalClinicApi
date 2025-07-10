using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Dtos
{
    public class CreateAppointmentDto
    {
        [Required(ErrorMessage = "El ID del paciente es obligatorio.")]
        public string PatientUserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ID del odontólogo es obligatorio.")]
        public string DentistProfileId  { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ID del servicio es obligatorio.")]
        public string ServiceId { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de la cita es obligatoria.")]
        public DateTime Date { get; set; } 

        public string Notes { get; set; } = string.Empty; 
    }
}