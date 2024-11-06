using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFirst.DTO;
using WebApiFirst.Models;

namespace WebApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTblsController : ControllerBase
    {
        private readonly EmpDeptDbContext _context;

        public EmployeeTblsController(EmpDeptDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeTbls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeeTbls()
        {
            var result = from em in _context.EmployeeTbls
                         select new EmployeeDTO {
                              DeptId=em.DeptId,
                              EmpId=em.EmpId,
                               EmpName=em.EmpName,
                               Salary=em.Salary,
                               LibId=em.LibId,
                         };
            return Ok(result);
        }

        // GET: api/EmployeeTbls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTbl>> GetEmployeeTbl(int id)
        {
            var employeeTbl = await _context.EmployeeTbls.FindAsync(id);

            if (employeeTbl == null)
            {
                return NotFound();
            }

            return employeeTbl;
        }

        // PUT: api/EmployeeTbls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeTbl(int id, EmployeeDTO employeeDTO)
        {
            EmployeeTbl employeeTbl = new EmployeeTbl();
            employeeTbl.EmpId = employeeDTO.EmpId;
            employeeTbl.EmpName = employeeDTO.EmpName;
            employeeTbl.Salary = employeeDTO.Salary;
            employeeTbl.DeptId = employeeDTO.DeptId;
            employeeTbl.LibId = employeeDTO.LibId;
            if (id != employeeTbl.EmpId)
            {
                return BadRequest();
            }

            _context.Entry(employeeTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTblExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeeTbls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeTbl>> PostEmployeeTbl(EmployeeDTO employeeDTO)
        {
            EmployeeTbl employeeTbl = new EmployeeTbl();
            employeeTbl.EmpId = employeeDTO.EmpId;
            employeeTbl.EmpName = employeeDTO.EmpName;
            employeeTbl.Salary = employeeDTO.Salary;
            employeeTbl.DeptId = employeeDTO.DeptId;
            employeeTbl.LibId = employeeDTO.LibId;
            _context.EmployeeTbls.Add(employeeTbl);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeTblExists(employeeTbl.EmpId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployeeTbl", new { id = employeeTbl.EmpId }, employeeTbl);
        }

        // DELETE: api/EmployeeTbls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeTbl(int id)
        {
            var employeeTbl = await _context.EmployeeTbls.FindAsync(id);
            if (employeeTbl == null)
            {
                return NotFound();
            }

            _context.EmployeeTbls.Remove(employeeTbl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeTblExists(int id)
        {
            return _context.EmployeeTbls.Any(e => e.EmpId == id);
        }
    }
}
