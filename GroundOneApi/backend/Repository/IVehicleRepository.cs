namespace backend.Repository;
//interface is like a menu
// u don't know how the food is prepared, you just know what you can order
using backend.Models;

/// Interface defining Vehicle operations on database
public interface IVehicleRepository
{
    Task<List<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(int id);
    Task<Vehicle?> GetByIdWithDetailsAsync(int id);
    Task<Vehicle> AddAsync(Vehicle vehicle);
    Task UpdateAsync(Vehicle vehicle);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);    
}
// its promise for service