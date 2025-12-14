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
    /// Gets list of all vehicles
    /// </summary>
    /// <returns>List of vehicles with basic info</returns>
    /// <response code="200">Returns list of vehicles</response>
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
    /// Pobiera szczegóły pojazdu o określonym ID
    /// </summary>
    /// <param name="id">ID pojazdu</param>
    /// <returns>Szczegółowe informacje o pojeździe wraz z przedziałami i wyposażeniem</returns>
    /// <response code="200">Zwraca pojazd</response>
    /// <response code="404">Pojazd nie został znaleziony</response>
    /// <response code="500">Błąd serwera</response>
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
    /// Tworzy nowy pojazd
    /// </summary>
    /// <param name="dto">Dane nowego pojazdu</param>
    /// <returns>Utworzony pojazd</returns>
    /// <response code="201">Pojazd został utworzony pomyślnie</response>
    /// <response code="400">Nieprawidłowe dane wejściowe</response>
    /// <response code="500">Błąd serwera</response>
    /// <remarks>
    /// Przykładowe żądanie:
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
    /// Aktualizuje istniejący pojazd
    /// </summary>
    /// <param name="id">ID pojazdu do zaktualizowania</param>
    /// <param name="dto">Zaktualizowane dane pojazdu</param>
    /// <returns>Zaktualizowany pojazd</returns>
    /// <response code="200">Pojazd został zaktualizowany</response>
    /// <response code="400">Nieprawidłowe dane wejściowe</response>
    /// <response code="404">Pojazd nie został znaleziony</response>
    /// <response code="500">Błąd serwera</response>
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
    /// Usuwa pojazd
    /// </summary>
    /// <param name="id">ID pojazdu do usunięcia</param>
    /// <returns>Brak zawartości</returns>
    /// <response code="204">Pojazd został usunięty</response>
    /// <response code="404">Pojazd nie został znaleziony</response>
    /// <response code="500">Błąd serwera</response>
    /// <remarks>
    /// UWAGA: Usunięcie pojazdu spowoduje również usunięcie wszystkich powiązanych 
    /// przedziałów i wyposażenia (cascade delete).
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

    /// <summary>
    /// Wyszukuje pojazdy po nazwie lub kryptonimie
    /// </summary>
    /// <param name="query">Termin wyszukiwania</param>
    /// <returns>Lista pasujących pojazdów</returns>
    /// <response code="200">Zwraca pasujące pojazdy</response>
    /// <response code="400">Pusty termin wyszukiwania</response>
    [HttpGet("search")]
    [ProducesResponseType(typeof(List<VehicleResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<VehicleResponseDto>>> Search([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest(new { message = "Termin wyszukiwania nie może być pusty" });
        }

        try
        {
            var vehicles = await _service.SearchVehiclesAsync(query);
            _logger.LogInformation("Search for '{Query}' returned {Count} results",
                query, vehicles.Count);
            return Ok(vehicles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching vehicles");
            return StatusCode(500, new { message = "Błąd podczas wyszukiwania" });
        }
    }

    /// <summary>
    /// Pobiera statystyki floty pojazdów
    /// </summary>
    /// <returns>Obiekt ze statystykami</returns>
    /// <response code="200">Zwraca statystyki</response>
    [HttpGet("stats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetStatistics()
    {
        try
        {
            var stats = await _service.GetVehicleStatisticsAsync();
            return Ok(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting vehicle statistics");
            return StatusCode(500, new { message = "Błąd podczas pobierania statystyk" });
        }
    }
}