using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.DTOs.Vehicles;
using FluentValidation;

namespace backend.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _service;
    private readonly IValidator<CreateVehicleDto> _createValidator;
    private readonly IValidator<UpdateVehicleDto> _updateValidator;
    private readonly ILogger<VehicleController> _logger;

    public VehicleController(
        IVehicleService service,
        IValidator<CreateVehicleDto> createValidator,
        IValidator<UpdateVehicleDto> updateValidator,
        ILogger<VehicleController> logger)
    {
        _service = service;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a list of all vehicles
    /// </summary>
    /// <returns>List of vehicles with basic information</returns>
    /// <response code="200">Returns the list of vehicles</response>
    /// <response code="500">Server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<VehicleResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<VehicleResponseDto>>> GetAll()
    {
        try
        {
            var vehicles = await _service.GetAllVehiclesAsync();
            _logger.LogInformation("Retrieved {Count} vehicles", vehicles.Count);
            return Ok(vehicles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all vehicles");
            return StatusCode(500, new { message = "Błąd podczas pobierania pojazdów" });
        }
    }

    /// <summary>
    /// Gets details of a vehicle by its ID
    /// </summary>
    /// <param name="id">Vehicle ID</param>
    /// <returns>Detailed vehicle information including compartments and equipment</returns>
    /// <response code="200">Returns the vehicle</response>
    /// <response code="404">Vehicle not found</response>
    /// <response code="500">Server error</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VehicleDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleDetailsDto>> GetById(int id)
    {
        try
        {
            var vehicle = await _service.GetVehicleByIdAsync(id);

            if (vehicle == null)
            {
                _logger.LogWarning("Vehicle with ID {VehicleId} not found", id);
                return NotFound(new { message = $"Pojazd o ID {id} nie został znaleziony" });
            }

            _logger.LogInformation("Retrieved vehicle {VehicleId}: {VehicleName}", id, vehicle.Name);
            return Ok(vehicle);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving vehicle {VehicleId}", id);
            return StatusCode(500, new { message = "Błąd podczas pobierania pojazdu" });
        }
    }

    /// <summary>
    /// Creates a new vehicle
    /// </summary>
    /// <param name="dto">New vehicle data</param>
    /// <returns>Created vehicle</returns>
    /// <response code="201">Vehicle successfully created</response>
    /// <response code="400">Invalid input data</response>
    /// <response code="500">Server error</response>
    /// <remarks>
    /// Example request:
    /// 
    ///     POST /api/vehicle
    ///     {
    ///        "name": "GBA 2/16 MAN",
    ///        "type": "GBA",
    ///        "cryptonym": "451-25",
    ///        "registrationNumber": "WE 1234",
    ///        "yearOfManufacture": 2020,
    ///        "status": "Available"
    ///     }
    /// 
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(VehicleResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleResponseDto>> Create([FromBody] CreateVehicleDto dto)
    {
        try
        {
            // Walidacja
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var vehicle = await _service.CreateVehicleAsync(dto);

            _logger.LogInformation("Created new vehicle with ID {VehicleId}: {VehicleName}",
                vehicle.Id, vehicle.Name);

            return CreatedAtAction(
                nameof(GetById),
                new { id = vehicle.Id },
                vehicle);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid argument when creating vehicle");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating vehicle");
            return StatusCode(500, new { message = "Błąd podczas tworzenia pojazdu" });
        }
    }

    /// <summary>
    /// Updates an existing vehicle
    /// </summary>
    /// <param name="id">ID of the vehicle to update</param>
    /// <param name="dto">Updated vehicle data</param>
    /// <returns>Updated vehicle</returns>
    /// <response code="200">Vehicle successfully updated</response>
    /// <response code="400">Invalid input data</response>
    /// <response code="404">Vehicle not found</response>
    /// <response code="500">Server error</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(VehicleResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleResponseDto>> Update(int id, [FromBody] UpdateVehicleDto dto)
    {
        try
        {
            // Walidacja
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var vehicle = await _service.UpdateVehicleAsync(id, dto);

            if (vehicle == null)
            {
                _logger.LogWarning("Vehicle with ID {VehicleId} not found for update", id);
                return NotFound(new { message = $"Pojazd o ID {id} nie został znaleziony" });
            }

            _logger.LogInformation("Updated vehicle {VehicleId}: {VehicleName}", id, vehicle.Name);
            return Ok(vehicle);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid argument when updating vehicle {VehicleId}", id);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating vehicle {VehicleId}", id);
            return StatusCode(500, new { message = "Błąd podczas aktualizacji pojazdu" });
        }
    }

    /// <summary>
    /// Deletes a vehicle
    /// </summary>
    /// <param name="id">ID of the vehicle to delete</param>
    /// <returns>No content</returns>
    /// <response code="204">Vehicle successfully deleted</response>
    /// <response code="404">Vehicle not found</response>
    /// <response code="500">Server error</response>
    /// <remarks>
    /// WARNING: Deleting a vehicle will also delete all related
    /// compartments and equipment (cascade delete).
    /// </remarks>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _service.DeleteVehicleAsync(id);

            if (!deleted)
            {
                _logger.LogWarning("Vehicle with ID {VehicleId} not found for deletion", id);
                return NotFound(new { message = $"Pojazd o ID {id} nie został znaleziony" });
            }

            _logger.LogInformation("Deleted vehicle {VehicleId}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting vehicle {VehicleId}", id);
            return StatusCode(500, new { message = "Błąd podczas usuwania pojazdu" });
        }
    }
}