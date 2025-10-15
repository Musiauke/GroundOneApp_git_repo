using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Services;
using backend.DTOs.Vehicle;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        // private readonly AppDbContext _context;
        // old way, direct access to Db, which is unsecure

        // interface service?
        private readonly IVehicleService _service;

        public VehicleController(IVehicleService service) // wcześniej AppDbContext context
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehicleResponseDto>>> GetAll()
        {
            var vehicles = await _service.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDetailsDto>> GetById(int id)
        {
            // here theres probably action from services
            var vehicle = await _service.GetVehicleByIdAsync(); // wheres the definition?
            if (vehicle == null)
                return NotFound(new { message = $"Vehicle with ID {id} was not found" });

            return Ok(vehicle);
        }
        // to jest chyba to co bylo przed GetVehicleByIdAsync

        //    var vehicle = await _context.Vehicles
        // .Include(v => v.Compartments)
        // .ThenInclude(c => c.Items)
        // .FirstOrDefaultAsync(v => v.Id == id);
        [HttpPost]
        public async Task<ActionResult<VehicleResponseDto>> Create(CreateVehicleDto dto)
        {
            try
            {
                var vehicle = await _service.CreateVehicleAsync(dto);
                return CreatedAction(nameof(GetById), new { id = vehicle.Id },
                vehicle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleResponseDto>> Update(int id, UpdateVehicleDto dto)
        {
            try
            {
                var vehicle = await _service.UpdateVehicleAsync(id, dto);

                if (vehicle == null)
                    return NotFound(new { message = $"Vehicle with ID {id} not found" });

                return Ok(vehicle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // nie powinno to tak wygladac?:
//         [HttpPut("{id}")]
// public async Task<IActionResult> UpdateVehicle(int id, UpdateVehicleDto dto)
// {
//     var vehicle = await _vehicleService.GetByIdAsync(id);
//     if (vehicle == null) return NotFound();

//     vehicle.Name = dto.Name;
//     vehicle.Type = dto.Type;
//     vehicle.Cryptonym = dto.Cryptonym;
//     vehicle.RegistrationNumber = dto.RegistrationNumber;
//     vehicle.YearOfManufacture = dto.YearOfManufacture;
//     vehicle.LastInspection = dto.LastInspection;
//     vehicle.NextInspection = dto.NextInspection;
//     vehicle.Status = dto.Status;
//     vehicle.Notes = dto.Notes;
//     // ewentualnie aktualizacja Compartments

//     await _vehicleService.UpdateAsync(vehicle);
//     return NoContent();
}


            /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteVehicleAsync(id);

            if (!deleted)
                return NotFound(new { message = $"Vehicle with ID {id} not found" });

            return NoContent(); // 
        }


    }
}