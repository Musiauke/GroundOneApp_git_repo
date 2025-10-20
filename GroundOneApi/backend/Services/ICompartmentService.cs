//interface , promise for controller
using backend.Models;
using backend.DTOs.Compartment;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public interface ICompartmentService
    {
        Task<IEnumerable<CompartmentDto>> GetAllAsync();
        Task<CompartmentDetailsDto?> GetByIdAsync(int id);
        Task<CompartmentDto> CreateAsync(CreateCompartmentDto dto); 
        Task<CompartmentDto?> UpdateAsync(int id, UpdateCompartmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}