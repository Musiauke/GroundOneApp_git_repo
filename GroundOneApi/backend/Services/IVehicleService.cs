namespace backend.Services;
//interface is like a menu
// u don't know how the food is prepared, you just know what you can order

using backend.DTOs.Vehicle;

public interface IVehicleService
{
    Task<List<VehicleResponseDto>> GetAllVehiclesAsync();
    Task<VehicleDetailsDto?> GetVehicleByIdAsync(int id);
    Task<VehicleResponseDto> CreateVehicleAsync(CreateVehicleDto dto);
    Task<VehicleResponseDto?> UpdateVehicleAsync(int id, UpdateVehicleDto dto);
    Task<bool> DeleteVehicleAsync(int id);
}

// its promise for controller 
