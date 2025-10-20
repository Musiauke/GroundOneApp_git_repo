using backend.Models;// for list / short data (GET all)

namespace backend.DTOs.Compartment
{
    public class CompartmentDto
    {
        public int Id { get; set; }    
        public string Name { get; set; } = string.Empty;
        public CompartmentLocation Location { get; set; }    

    }
}