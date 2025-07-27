using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Dtos
{
    public class ClinicalRecordDetailedDto
    {
        public string Id { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = null!;
        public string Treatment { get; set; } = null!;
        public string Notes { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public AppoinmentMiniDtoCr Appointment { get; set; } = null!;
        public PatientMiniDtoCr Patient { get; set; } = null!;
        public DentistMiniDtoCr Dentist { get; set; } = null!;
    }

    public class AppoinmentMiniDtoCr
    {
        public string Id { get; set; } = null!;
        public DateTime Date { get; set; }

        public AppointmentStatus Status { get; set; }
        public ServiceDtoCr Service { get; set; } = null!;
    }

    public class ServiceDtoCr
    {
        public string Id { get; set; } = null!;
        public string Name  { get; set; } = null!;
        public decimal Price { get; set; }
    }

    public class PatientMiniDtoCr
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }

    public class DentistMiniDtoCr
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Specialty { get; set; } = null!;
    }


}