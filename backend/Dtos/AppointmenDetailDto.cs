namespace DentalClinicApi.Dtos
{
    public class AppointmentDetailDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Notes { get; set; } = string.Empty;

        public PatientMiniDto Patient { get; set; } = default!;
        public DentistMiniDto Dentist { get; set; } = default!;
        public ServiceMiniDto Service { get; set; } = default!;
    }

    public class PatientMiniDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class DentistMiniDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
    }

    public class ServiceMiniDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
