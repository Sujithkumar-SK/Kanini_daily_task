using Hospital_Management.DTOs;
using Hospital_Management.Models;
using Hospital_Management.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly HospitalService _hospitalService;

        public HospitalsController(HospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        // ✅ Anyone can view all hospitals
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospitals()
        {
            return Ok(await _hospitalService.GetAllAsync());
        }

        // ✅ Anyone can view hospital details
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Hospital>> GetHospital(string id)
        {
            var hospital = await _hospitalService.GetByIdAsync(id);
            if (hospital == null) return NotFound();
            return Ok(hospital);
        }

        // ✅ Only Admin can create new hospitals
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateHospital([FromBody] HospitalDTO dto)
        {
            var hospital = new Hospital
            {
                Name = dto.Name
            };

            await _hospitalService.AddAsync(hospital);
            return Ok(hospital);
        }

        // ✅ Only Admin can update hospitals
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateHospital(string id, [FromBody] HospitalDTO dto)
        {
            var hospital = await _hospitalService.GetByIdAsync(id);
            if (hospital == null) return NotFound();

            hospital.Name = dto.Name;

            await _hospitalService.UpdateAsync(hospital);
            return NoContent();
        }

        // ✅ Only Admin can delete hospitals
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteHospital(string id)
        {
            var hospital = await _hospitalService.GetByIdAsync(id);
            if (hospital == null) return NotFound();

            await _hospitalService.DeleteAsync(id);
            return NoContent();
        }
    }
}
