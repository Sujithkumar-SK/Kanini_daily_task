using Hospital_Management.DTOs;
using Hospital_Management.Models;
using Hospital_Management.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hospital_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientsController(PatientService patientService)
        {
            _patientService = patientService;
        }

        // ✅ Only Admin & Doctor can view all patients
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            return Ok(await _patientService.GetAllAsync());
        }

        // ✅ Admin & Doctor can view any patient
        // ✅ Patient can only view their own record
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<ActionResult<Patient>> GetPatient(string id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound();

            // If role is Patient, check if they are requesting their own record
            if (User.IsInRole("Patient"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != id) return Forbid();
            }

            return Ok(patient);
        }

        // ✅ Admin can create new patients
        // (Optional: allow self-registration by replacing with [AllowAnonymous])
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreatePatient([FromBody] PatientDTO dto)
        {
            var patient = new Patient
            {
                Name = dto.Name,
                HospitalId = dto.HospitalId
            };
            await _patientService.AddAsync(patient);
            return Ok(patient);
        }

        // ✅ Admin can update any patient
        // ✅ Patient can update only their own profile
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<ActionResult> UpdatePatient(string id, [FromBody] PatientDTO dto)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound();

            // Patient role can only update their own record
            if (User.IsInRole("Patient"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != id) return Forbid();
            }

            patient.Name = dto.Name;
            patient.HospitalId = dto.HospitalId;

            await _patientService.UpdateAsync(patient);
            return NoContent();
        }

        // ✅ Only Admin can delete patients
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeletePatient(string id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound();

            await _patientService.DeleteAsync(id);
            return NoContent();
        }
    }
}
