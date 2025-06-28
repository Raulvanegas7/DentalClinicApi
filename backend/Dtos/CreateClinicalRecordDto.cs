using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalClinicApi.Dtos
{
    public class CreateClinicalRecordDto
    {
        public string AppointmentId { get; set; } = null!;
        public string PatientId { get; set; } = null!;
        public string DentistId { get; set; } = null!;
        public string Diagnosis { get; set; } = null!;
        public string Treatment { get; set; } = null!;
        public string Notes { get; set; } = null!;
    }
}