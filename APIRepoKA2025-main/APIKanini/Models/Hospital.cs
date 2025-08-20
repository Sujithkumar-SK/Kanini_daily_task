using System.Numerics;

namespace APIKanini.Models
{
    public class Hospital
    {
        public string HospitalId { get; set; }
        public string? Name { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
        public ICollection<Patient>? Patients { get; set; }
    }
}
