using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public class Hospital
    {
        [Key]
        public string HospitalId { get; set; }
        public string? Name { get; set; }

        public ICollection<Doctor>? Doctors { get; set; }
        public ICollection<Patient>? Patients { get; set; }
    }
}
