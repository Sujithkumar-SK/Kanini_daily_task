using Hospital_Management.DTOs;
using Hospital_Management.Models;
using Hospital_Management.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorsController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // ✅ Anyone (even without login) can view all doctors
        [HttpGet]
        //[AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return Ok(await _doctorService.GetAllAsync());
        }

        // ✅ Only Admin & Doctor can view doctor details
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult<Doctor>> GetDoctor(string id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        // ✅ Only Admin can create new doctors
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateDoctor([FromBody] DoctorDto dto)
        {
            var doctor = new Doctor
            {
                Name = dto.Name,
                Specialization = dto.Specialization,
                HospitalId = dto.HospitalId
            };
            await _doctorService.AddAsync(doctor);
            return Ok(doctor);
        }

        // ✅ Admin and Doctor can update (you can refine later to allow only the doctor themselves)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult> UpdateDoctor(string id, [FromBody] DoctorDto dto)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null) return NotFound();

            doctor.Name = dto.Name;
            doctor.Specialization = dto.Specialization;
            doctor.HospitalId = dto.HospitalId;

            await _doctorService.UpdateAsync(doctor);
            return NoContent();
        }

        // ✅ Only Admin can delete doctors
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteDoctor(string id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null) return NotFound();

            await _doctorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
