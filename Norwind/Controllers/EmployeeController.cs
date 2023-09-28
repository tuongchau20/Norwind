using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Norwind.Data;
using Norwind.DTO;
using Norwind.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Norwind.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly NorthwindContext _context;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(NorthwindContext context, ILogger<EmployeeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Select(e => new EmployeeDTO
                {
                    EmployeeId = e.EmployeeId,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    BirthDate = e.BirthDate,
                    Photo = e.Photo,
                    Notes = e.Notes
                })
                .ToListAsync();

            return Ok(employees);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Where(e => e.EmployeeId == id)
                .Select(e => new EmployeeDTO
                {
                    EmployeeId = e.EmployeeId,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    BirthDate = e.BirthDate,
                    Photo = e.Photo,
                    Notes = e.Notes
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(EmployeeDTO employeeDTO)
        {
            var employee = new Employee
            {
                LastName = employeeDTO.LastName,
                FirstName = employeeDTO.FirstName,
                BirthDate = employeeDTO.BirthDate,
                Photo = employeeDTO.Photo,
                Notes = employeeDTO.Notes
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO employeeDTO)
        {
            var existingEmployee = await _context.Employees.FindAsync(id);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.LastName = employeeDTO.LastName;
            existingEmployee.FirstName = employeeDTO.FirstName;
            existingEmployee.BirthDate = employeeDTO.BirthDate;
            existingEmployee.Photo = employeeDTO.Photo;
            existingEmployee.Notes = employeeDTO.Notes;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
