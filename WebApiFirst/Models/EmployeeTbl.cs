using System;
using System.Collections.Generic;

namespace WebApiFirst.Models;

public partial class EmployeeTbl
{
    public int EmpId { get; set; }

    public string EmpName { get; set; }

    public int Salary { get; set; }

    public int DeptId { get; set; }

    public int? LibId { get; set; }

    public virtual DepartmentTbl Dept { get; set; } = null!;

    public virtual Library? Lib { get; set; }
}
