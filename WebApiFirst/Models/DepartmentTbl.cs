using System;
using System.Collections.Generic;

namespace WebApiFirst.Models;

public partial class DepartmentTbl
{
    public int DeptId { get; set; }

    public string DeptName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<EmployeeTbl> EmployeeTbls { get; set; } = new List<EmployeeTbl>();
}
