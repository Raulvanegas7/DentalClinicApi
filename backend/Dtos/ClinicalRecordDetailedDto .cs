using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos
{
    public class ClinicalRecordDetailedDto
    {
        public string Id { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppoinmentMiniDto Appointment { get; set; }
        public PatientMiniDto Patient { get; set; }
        public DentistMiniDto Dentist { get; set; }
    }

    public class AppoinmentMiniDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public ServiceDto Service { get; set; }
    }

    public class ServiceDto
    {
        public string Id { get; set; }
        public string Name  { get; set; }
        public decimal Price { get; set; }
    }

    public class PatientMiniDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class DentistMiniDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
    }


}