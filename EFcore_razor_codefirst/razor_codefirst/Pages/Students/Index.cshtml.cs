using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor_codefirst.Data;
using razor_codefirst.Models;

namespace razor_codefirst.Pages_Students
{
    public class IndexModel : PageModel
    {
        private readonly razor_codefirst.Data.StudentContext _context;

        public IndexModel(razor_codefirst.Data.StudentContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Student = await _context.Students.ToListAsync();
        }
    }
}
