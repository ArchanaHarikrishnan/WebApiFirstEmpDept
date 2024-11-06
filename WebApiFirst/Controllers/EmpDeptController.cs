using Microsoft.AspNetCore.Mvc;
using WebApiFirst.Models;

namespace WebApiFirst.Controllers
{
    public class DeptEmpResult
    {
        public int DeptId { get; set; }
        public int EmpId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class EmpDeptController : ControllerBase
    {
        private readonly EmpDeptDbContext _context;

        public EmpDeptController(EmpDeptDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<DeptEmpResult> GetEmpDept()
        {

            var result = (from emp in _context.EmployeeTbls
                          join dept in _context.DepartmentTbls on emp.DeptId equals dept.DeptId
                          select new DeptEmpResult { EmpId = emp.EmpId, DeptId = dept.DeptId }).ToList();
            return result;
        }

    }
}