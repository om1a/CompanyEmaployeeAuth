using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DataContext _context;

        public CompanyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<List<Company>>> Get()
        {
            return Ok(await _context.Companys.ToListAsync());
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Company>> Get(int id)
        {
            var comp = await _context.Companys.FindAsync(id);
            if (comp == null)
                return BadRequest("Company not found.");
            return Ok(comp);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<List<Company>>> Addcomp(Company comp)
        {
            _context.Companys.Add(comp);
            _context.SaveChanges();

            return Ok(await _context.Companys.ToListAsync());
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<List<Company>>> Updatecomp(Company request)
        {
            var dbCompany = _context.Companys.Find(request.Id);
            if (dbCompany == null)
                return BadRequest("Company not found.");

            dbCompany.Name = request.Name;
            dbCompany.Id = request.Id;
            dbCompany.Location = request.Location;

            _context.SaveChanges();

            return Ok(_context.Companys.ToList());
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<List<Company>>> Delete(int id)
        {
            var dbcomp = _context.Companys.Find(id);
            if (dbcomp == null)
                return BadRequest("Company not found.");

            _context.Companys.Remove(dbcomp);
            _context.SaveChanges();

            return Ok(_context.Companys.ToList());
        }

    }
}
