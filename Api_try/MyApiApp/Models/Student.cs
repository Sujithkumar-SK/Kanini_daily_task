using System;
using System.Collections.Generic;

namespace MyApiApp.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();
}
