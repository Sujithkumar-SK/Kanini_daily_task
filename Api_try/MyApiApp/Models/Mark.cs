using System;
using System.Collections.Generic;

namespace MyApiApp.Models;

public partial class Mark
{
    public int MarkId { get; set; }

    public string? Subject { get; set; }

    public int? Score { get; set; }

    public int? StudentId { get; set; }

    public virtual Student? Student { get; set; }
}
