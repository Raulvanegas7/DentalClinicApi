using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalClinicApi.Configurations
{
    public class DataBaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PatientsCollectionName { get; set; } = null!;
        public string DentistsCollectionName { get; set; } = null!;
        public string ServicesCollectionName { get; set; } = null!;
    }
}