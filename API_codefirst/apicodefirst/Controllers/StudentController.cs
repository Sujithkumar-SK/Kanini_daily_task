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
  [HttpPost]
  public async Task<ActionResult<Student>> Post([FromBody]Student student)
  {
      return Ok(await _studentService.AddStudent(student));
  }
  [HttpPut("{id}")]
  public async Task<ActionResult<Student>> Put(int id, [FromBody]Student student)
  {
    if (student == null || id != student.Id)
    {
      return BadRequest("Invalid student data");
    }
    else
    {
      var stu = await _studentService.UpdateStudent(id, student);
      if (stu == null)
      {
        return NotFound("Student not found");
      }
      else
      {
        return Ok(stu);
      }
    }
  }
}