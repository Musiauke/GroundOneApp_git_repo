//interface

namespace backend.Repository;
using backend.Models;

public interface ICompartmentRepository
{

    Task<IEnumerable<Compartment>> GetAllAsync();
    Task<Compartment?> GetByIdAsync(int id);
    Task<IEnumerable<Compartment>> GetByCompartmentIdAsync(int compartmentId);
    Task<Compartment> AddAsync(Compartment compartment);
    Task<Compartment> UpdateAsync(Compartment compartment);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);

}


