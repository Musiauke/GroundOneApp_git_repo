// implementation 
using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Data;

namespace backend.Repository;

public class CompartmentRepository : ICompartmentRepository
{
    private readonly AppDbContext _context;

    public CompartmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Compartment>> GetAllAsync()
    {
        return await _context.Compartments
            .Include(c => c.Vehicle)
            .Include(c => c.Items)
            .ToListAsync();
    }

    public async Task<Compartment?> GetByIdAsync(int id)
    {
        return await _context.Compartments
            .Include(c => c.Vehicle)
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id);    
    }

    public async Task<IEnumerable<Compartment>> GetByCompartmentIdAsync(int compartmentId)
    {
        return await _context.Compartments
            .Include(c => c.Items)
            .Where(c => c.Id == compartmentId)
            .ToListAsync();
    }

    public async Task<Compartment> AddAsync(Compartment compartment)
    { // a.k.a. Create
        await _context.Compartments.AddAsync(compartment);
        await _context.SaveChangesAsync();
        return compartment;
    }

    public async Task<Compartment> UpdateAsync(Compartment compartment)
    {
        _context.Compartments.Update(compartment);
        await _context.SaveChangesAsync();
        return compartment;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var compartment = await _context.Compartments.FindAsync(id);
        if (compartment == null)
        {
            return false;
        }

        _context.Compartments.Remove(compartment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Compartments.AnyAsync(c => c.Id == id);
    }

}