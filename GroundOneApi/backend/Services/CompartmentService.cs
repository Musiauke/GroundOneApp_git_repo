using backend.Models;
using backend.DTOs.Compartment;
using Microsoft.EntityFrameworkCore;
using backend.Data;

namespace backend.Services
{
    public class CompartmentService : ICompartmentService
    {
        private readonly AppDbContext _context;

        public CompartmentService(AppDbContext context)
        {
            _context = context;
        }

        // GET all
        public async Task<IEnumerable<CompartmentDto>> GetAllAsync()
        {
            var compartments = await _context.Compartments
                .Include(c => c.Items)
                .ToListAsync();

            return compartments.Select(c => new CompartmentDto
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location
            });
        }

        // GET by id
        public async Task<CompartmentDetailsDto?> GetByIdAsync(int id)
        {
            var c = await _context.Compartments
                .Include(x => x.Items)
                .Include(x => x.Vehicle)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (c == null) return null;

            return new CompartmentDetailsDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Location = c.Location,
                VehicleId = c.VehicleId
            };
        }

        // CREATE - ZMIENIONE: zwraca DTO zamiast Model
        public async Task<CompartmentDto> CreateAsync(CreateCompartmentDto dto)
        {
            var compartment = new Compartment
            {
                Name = dto.Name,
                Description = dto.Description,
                Location = dto.Location,
                VehicleId = dto.VehicleId
            };

            _context.Compartments.Add(compartment);
            await _context.SaveChangesAsync();

            // Zwróć DTO zamiast Model
            return new CompartmentDto
            {
                Id = compartment.Id,
                Name = compartment.Name,
                Location = compartment.Location
            };
        }

        // UPDATE
        public async Task<CompartmentDto?> UpdateAsync(int id, UpdateCompartmentDto dto)
        {
            var c = await _context.Compartments.FindAsync(id);
            if (c == null) return null;

            c.Name = dto.Name;
            c.Description = dto.Description;
            c.Location = dto.Location;

            await _context.SaveChangesAsync();

            return new CompartmentDto
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location
            };
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var c = await _context.Compartments.FindAsync(id);
            if (c == null) return false;

            _context.Compartments.Remove(c);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}