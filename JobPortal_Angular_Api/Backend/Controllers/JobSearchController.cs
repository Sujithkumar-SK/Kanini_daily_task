using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Candidate,Admin,Recruiter")] // only candidates can search
    public class JobSearchController : ControllerBase
    {
        private readonly IJobSearchService _service;

        public JobSearchController(IJobSearchService service)
        {
            _service = service;
        }

        // POST: api/jobs/search
        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] JobSearchDto dto)
        {
            var results = await _service.SearchJobsAsync(dto);
            return Ok(results);
        }
    }
}
