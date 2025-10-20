using Microsoft.AspNetCore.Mvc;
using backend.DTOs.Items;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _service;

        public ItemsController(IItemService service)
        {
            _service = service;  // ← Było "_context = context;" - ŹLE!
        }

        // GET all items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            var items = await _service.GetAllItemsAsync();
            return Ok(items);
        }

        // GET item by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDetailsDto>> GetItem(int id)
        {
            var item = await _service.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // CREATE item
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem(CreateItemDto dto)
        {
            var created = await _service.CreateItemAsync(dto);
            return CreatedAtAction(nameof(GetItem), new { id = created.Id }, created);
        }

        // UPDATE item
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDto>> PutItem(int id, UpdateItemDto dto)
        {
            var updated = await _service.UpdateItemAsync(id, dto);
            if (updated == null)
            {
                return NotFound();
            }
            return Ok(updated);
        }

        // DELETE item
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _service.DeleteItemAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}