using APIKanini.DTOs;
using APIKanini.Models;
using APIKanini.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIKanini.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorsController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return Ok(await _doctorService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(string id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDoctor([FromBody] DoctorDto dto)
        {
            //mapping of DTO with model
            var doctor = new Doctor
            {
                Name = dto.Name,
                Specialization = dto.Specialization,
                HospitalId = dto.HospitalId
            };
            await _doctorService.AddAsync(doctor);
            return Ok(doctor);
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(string id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null) return NotFound();

            await _doctorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
