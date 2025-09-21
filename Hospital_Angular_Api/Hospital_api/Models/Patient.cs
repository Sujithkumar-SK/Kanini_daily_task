using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public class Patient
    {
        [Key]
        public string PatientId { get; set; }
        public string? Name { get; set; }

        public string HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        // Many-to-Many with Doctors
        public ICollection<Doctor>? Doctors { get; set; }
    }
}
