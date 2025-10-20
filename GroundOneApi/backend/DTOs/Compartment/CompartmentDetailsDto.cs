using backend.Models;
namespace backend.DTOs.Compartment
{
    public class CompartmentDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CompartmentLocation Location { get; set; }

        public int VehicleId { get; set; } // Foreign key
        public Vehicle Vehicle { get; set; } = null!; // Navigation property MANY TO ONE
    }
}