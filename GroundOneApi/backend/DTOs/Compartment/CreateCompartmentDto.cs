using backend.Models;

namespace backend.DTOs.Compartment
{
    public class CreateCompartmentDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CompartmentLocation Location { get; set; }
        public int VehicleId { get; set; } // Foreign key - user must tell to which vehicle this compartment belongs
    }
}