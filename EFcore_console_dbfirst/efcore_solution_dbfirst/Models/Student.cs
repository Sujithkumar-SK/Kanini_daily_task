using System;
using System.Collections.Generic;

namespace efcore_solution_dbfirst.Models;

public partial class Student
{
    public int StdId { get; set; }

    public string? Name { get; set; }

    public int RollNo { get; set; }

    public int? Age { get; set; }

    public string? Place { get; set; }

}
