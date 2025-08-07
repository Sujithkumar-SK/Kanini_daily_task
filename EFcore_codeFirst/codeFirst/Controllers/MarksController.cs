using Microsoft.AspNetCore.Mvc;
using codeFirst.Models;
namespace codeFirst.Controllers;

public class MarksController : Controller
{
  private readonly StudentContext _context;
  public MarksController(StudentContext context)
  {
    _context = context;
  }
  public IActionResult Index()
  {
    IEnumerable<Marks> marks = _context.Marks.ToList();
    return View(marks);
  }
  public IActionResult Details(int id)
  {
    Marks? mark = _context.Marks.FirstOrDefault(m => m.MarksId == id);
    if (mark == null)
    {
      return NotFound();
    }
    return View(mark);
  }
  public IActionResult Create()
  {
    return View();
  }
  [HttpPost]
  public IActionResult Create(Marks marks)
  {
    _context.Marks.Add(marks);
    _context.SaveChanges();
    return RedirectToAction("Index");
  }
  public IActionResult Edit(int id)
  {
    Marks? mark = _context.Marks.FirstOrDefault(m => m.MarksId == id);
    if (mark == null)
    {
      return NotFound();
    }
    return View(mark);
  }
  [HttpPost]
  public IActionResult Edit(int id, Marks newmark)
  {
    Marks? mark = _context.Marks.FirstOrDefault(m => m.MarksId == id);
    mark!.Subject = newmark.Subject;
    _context.SaveChanges();
    return RedirectToAction("Index");
  }
  public IActionResult Delete(int id)
  {
    Marks? mark = _context.Marks.FirstOrDefault(m => m.MarksId == id);
    if (mark == null)
    {
      return NotFound();
    }
    return View(mark);
  }
  [HttpPost]
  public IActionResult Delete(int id, Marks newmark)
  {
    Marks? mark = _context.Marks.FirstOrDefault(m => m.MarksId == id);
    _context.Marks.Remove(mark!);
    _context.SaveChanges();
    return RedirectToAction("Index");
  }
}