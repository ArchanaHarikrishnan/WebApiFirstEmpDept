using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFirst.Models;
using WebApiFirst.DTO;

namespace WebApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentTblsController : ControllerBase
    {
        private readonly EmpDeptDbContext _context;

        public DepartmentTblsController(EmpDeptDbContext context)
        {
            _context = context;
        }

        // GET: api/DepartmentTbls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentTbl>>> GetDepartmentTbls()
        {
            return await _context.DepartmentTbls.ToListAsync();
        }

        // GET: api/DepartmentTbls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentTbl>> GetDepartmentTbl(int id)
        {
            var departmentTbl = await _context.DepartmentTbls.FindAsync(id);

            if (departmentTbl == null)
            {
                return NotFound();
            }

            return departmentTbl;
        }

        // PUT: api/DepartmentTbls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartmentTbl(int id, DepartmentTbl departmentTbl)
        {
            if (id != departmentTbl.DeptId)
            {
                return BadRequest();
            }

            _context.Entry(departmentTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentTblExists(id))
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

        // POST: api/DepartmentTbls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DepartmentTbl>> PostDepartmentTbl(DeptDTO deptDTO)
        {
            DepartmentTbl departmentTbl = new DepartmentTbl();
            departmentTbl.DeptId = deptDTO.DeptId;
            departmentTbl.DeptName = deptDTO.DeptName;
            departmentTbl.Location = deptDTO.Location;
            _context.DepartmentTbls.Add(departmentTbl);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DepartmentTblExists(departmentTbl.DeptId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDepartmentTbl", new { id = departmentTbl.DeptId }, departmentTbl);
        }

        // DELETE: api/DepartmentTbls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentTbl(int id)
        {
            var departmentTbl = await _context.DepartmentTbls.FindAsync(id);
            if (departmentTbl == null)
            {
                return NotFound();
            }

            _context.DepartmentTbls.Remove(departmentTbl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentTblExists(int id)
        {
            return _context.DepartmentTbls.Any(e => e.DeptId == id);
        }
    }
}
