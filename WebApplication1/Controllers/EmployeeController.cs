using WebApplication1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
                return BadRequest("Employee not found.");
            return Ok(emp);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<List<Employee>>> Addemp(Employee emp)
        {
             _context.Employees.Add(emp);
            _context.SaveChanges();

            return Ok(_context.Employees.ToList());
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<List<Employee>>> Updateemp(Employee employeeD)
        {
            var dbEmployees = _context.Employees.Find(employeeD.Id);
            if (dbEmployees == null)
                return BadRequest("Employee not found.");

            dbEmployees.Name = employeeD.Name;
            dbEmployees.Id = employeeD.Id;
            dbEmployees.Age = employeeD.Age;
            dbEmployees.Location = employeeD.Location;
            dbEmployees.CompID = employeeD.CompID;

            _context.SaveChanges();

            return Ok(_context.Employees.ToList());
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<List<Employee>>> Delete(int id)
        {
            var dbemp = _context.Employees.Find(id);
            if (dbemp == null)
                return BadRequest("Employee not found.");

            _context.Employees.Remove(dbemp);
            _context.SaveChanges();

            return Ok(_context.Employees.ToList());
        }
    }
}
