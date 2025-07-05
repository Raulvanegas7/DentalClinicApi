using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos
{
    public class ClinicalRecordDetailedDto
    {
        public string Id { get; set; } = string.Empty;
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppoinmentMiniDtoCr Appointment { get; set; }
        public PatientMiniDtoCr Patient { get; set; }
        public DentistMiniDtoCr Dentist { get; set; }
    }

    public class AppoinmentMiniDtoCr
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public ServiceDtoCr Service { get; set; }
    }

    public class ServiceDtoCr
    {
        public string Id { get; set; }
        public string Name  { get; set; }
        public decimal Price { get; set; }
    }

    public class PatientMiniDtoCr
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class DentistMiniDtoCr
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
    }


}