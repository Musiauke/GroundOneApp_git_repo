using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;

    public ItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _context.Items
            .Include(i => i.Compartment)
                .ThenInclude(c => c!.Vehicle)
            .ToListAsync();
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        return await _context.Items
            .Include(i => i.Compartment)
                .ThenInclude(c => c!.Vehicle)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Item>> GetByCompartmentIdAsync(int compartmentId)
    {
        return await _context.Items
            .Include(i => i.Compartment)
                .ThenInclude(c => c!.Vehicle)
            .Where(i => i.CompartmentId == compartmentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Item>> GetByVehicleIdAsync(int vehicleId)
    {
        return await _context.Items
            .Include(i => i.Compartment)
                .ThenInclude(c => c!.Vehicle)
            .Where(i => i.Compartment != null && i.Compartment.VehicleId == vehicleId)  // check null
            .ToListAsync();
    }

    public async Task<IEnumerable<Item>> GetByStatusAsync(string status)
    {
        if (!Enum.TryParse<ItemStatus>(status, true, out var statusEnum))
        {
            return Enumerable.Empty<Item>();
        }
        
        return await _context.Items
            .Include(i => i.Compartment)
                .ThenInclude(c => c!.Vehicle)
            .Where(i => i.Status == statusEnum)
            .ToListAsync();
    }

    public async Task<IEnumerable<Item>> GetByCategoryAsync(string category)
    {
        if (!Enum.TryParse<EquipmentCategory>(category, true, out var categoryEnum))
        {
            return Enumerable.Empty<Item>();
        }
        
        return await _context.Items
            .Include(i => i.Compartment)
                .ThenInclude(c => c!.Vehicle)
            .Where(i => i.Category == categoryEnum)
            .ToListAsync();
    }

    public async Task<Item> AddAsync(Item item)
    {
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<Item> UpdateAsync(Item item)
    {
        _context.Items.Update(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return false;
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Items.AnyAsync(i => i.Id == id);
    }
}