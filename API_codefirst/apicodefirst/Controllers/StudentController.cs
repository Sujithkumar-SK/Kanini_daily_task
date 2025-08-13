using Microsoft.AspNetCore.Mvc;
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
  private readonly IStudent _studentService;
  public StudentController(IStudent studentService)
  {
    _studentService = studentService;
  }
  [HttpGet]
  public async Task<IEnumerable<Student>> Get()
  {
    return await _studentService.GetAllStudents();
  }
  [HttpGet("{id}")]
  public async Task<ActionResult<Student>> Get(int id)
  {
    var std = await _studentService.GetStudentById(id);
    if (std == null)
    {
      return NotFound();
    }
    return Ok(std);
  }
}