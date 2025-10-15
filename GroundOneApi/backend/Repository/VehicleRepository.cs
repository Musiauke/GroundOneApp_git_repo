namespace backend.Repository;

using backend.Data; // your DbContext namespace
using backend.Models;
using Microsoft.EntityFrameworkCore;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _context;
    
    // Dependency Injection - we add database context
    public VehicleRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    // Now we implement all methods from the interface:

    public async Task<List<Vehicle>> GetAllAsync()
    {
        return await _context.Vehicles.ToListAsync();
    }
    
    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await _context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == id);
    }
    
    public async Task<Vehicle?> GetByIdWithDetailsAsync(int id)
    {
        var vehicle = await _context.Vehicles
            .Include(v => v.Compartments)
            .ThenInclude(c => c.Items)
            .FirstOrDefaultAsync(v => v.Id == id);
        return vehicle;
    }
    
    public async Task<Vehicle> AddAsync(Vehicle vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
        await _context.SaveChangesAsync();
        return vehicle;
    }
    
    public async Task UpdateAsync(Vehicle vehicle)
    {
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle != null)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Vehicles.AnyAsync(v => v.Id == id);
    }
}