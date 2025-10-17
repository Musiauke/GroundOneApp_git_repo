using backend.Models;

namespace backend.Repository;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllAsync();
    Task<Item?> GetByIdAsync(int id);
    Task<IEnumerable<Item>> GetByCompartmentIdAsync(int compartmentId);
    Task<IEnumerable<Item>> GetByVehicleIdAsync(int vehicleId);
    Task<IEnumerable<Item>> GetByStatusAsync(string status);
    Task<IEnumerable<Item>> GetByCategoryAsync(string category);
    Task<Item> AddAsync(Item item);
    Task<Item> UpdateAsync(Item item);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}