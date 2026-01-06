using Microsoft.AspNetCore.Mvc;
using backend.DTOs.Compartment;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/compartments")]
    public class CompartmentController : ControllerBase
    {
        private readonly ICompartmentService _service;

        public CompartmentController(ICompartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompartmentDto>>> GetCompartments()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompartmentDetailsDto>> GetCompartment(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompartment(CreateCompartmentDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCompartment), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CompartmentDto>> Update(int id, UpdateCompartmentDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}