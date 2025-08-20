namespace APIKanini.Models
{
    public class Doctor
    {
        public string DoctorId { get; set; }
        public string? Name { get; set; }
        public string? Specialization { get; set; }

        public string HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        // Many-to-Many with Patients
        public ICollection<Patient>? Patients { get; set; }
    }
}
