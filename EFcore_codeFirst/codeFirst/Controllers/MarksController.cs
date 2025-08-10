using Microsoft.AspNetCore.Mvc;
using codeFirst.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace codeFirst.Controllers;

public class MarksController : Controller
{
  private readonly StudentContext _context;
  public MarksController(StudentContext context)
  {
    _context = context;
  }
  public async Task<IActionResult> Index()
  {
    var marks = await _context.Marks
                              .Include(m => m.Student)
                              .ToListAsync();
    return View(marks);
  }

  public async Task<IActionResult> Details(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var student = await _context.Students
        .Include(s => s.Marks)
        .FirstOrDefaultAsync(m => m.Id == id);

    if (student == null)
    {
      return NotFound();
    }

    return View(student);
  }

  public IActionResult Create()
  {
    ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
    return View();
  }
  [HttpPost]
  public IActionResult Create(Marks marks)
  {
    if (ModelState.IsValid)
    {
      _context.Marks.Add(marks);
      _context.SaveChanges();
      return RedirectToAction(nameof(Index));
    }
    ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", marks.StudentId);
    return View(marks);
  }
  public IActionResult Edit(int id)
  {
    var mark = _context.Marks.Find(id);
    if (mark == null)
    {
      return NotFound();
    }
    ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", mark.StudentId);
    return View(mark);
  }
  [HttpPost]
  public IActionResult Edit(int id, Marks marks)
  {
    if (id != marks.MarksId)
    {
      return NotFound();
    }

    if (ModelState.IsValid)
    {
      _context.Update(marks);
      _context.SaveChanges();
      return RedirectToAction(nameof(Index));
    }
    ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", marks.StudentId);
    return View(marks);
  }
  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var student = await _context.Students
        .Include(s => s.Marks)
        .FirstOrDefaultAsync(m => m.Id == id);

    if (student == null)
    {
      return NotFound();
    }

    return View(student);
  }

  [HttpPost]
  public IActionResult DeleteConfirmed(int id)
  {
    var mark = _context.Marks.Find(id);
    if (mark != null)
    {
      _context.Marks.Remove(mark);
      _context.SaveChanges();
    }
    return RedirectToAction(nameof(Index));
  }
}