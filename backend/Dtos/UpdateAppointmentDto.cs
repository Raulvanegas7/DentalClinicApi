using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Dtos
{
    public class UpdateAppointmentDto
    {
        [Required(ErrorMessage = "La fecha de la cita es obligatoria.")]
        public DateTime Date { get; set; }

        public string Notes { get; set; } = string.Empty;

        public AppointmentStatus? Status { get; set; }
    }
}