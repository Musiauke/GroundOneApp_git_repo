using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompartmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compartment>>> GetCompartments()
        {
            return await _context.Compartments.Include(c => c.Items).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Compartment>> GetCompartment(int id)
        {
            var compartment = await _context.Compartments
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compartment == null)
            {
                return NotFound();
            }

            return compartment;
        }

        [HttpPost] // actually not neeeded cuz u will have fixed list of it, but maybe if u want really custom its ok, dunno
        public async Task<ActionResult<Compartment>> CreateCompartment(Compartment compartment)
        {
            _context.Compartments.Add(compartment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompartment), new { id = compartment.Id }, compartment);
        }
    }
}