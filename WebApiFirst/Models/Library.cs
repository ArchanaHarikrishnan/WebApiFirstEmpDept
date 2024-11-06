using System;
using System.Collections.Generic;

namespace WebApiFirst.Models;

public partial class Library
{
    public int LibId { get; set; }

    public string LibName { get; set; } = null!;

    public virtual ICollection<EmployeeTbl> EmployeeTbls { get; set; } = new List<EmployeeTbl>();
}
