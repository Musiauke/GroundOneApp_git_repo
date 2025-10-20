using System.ComponentModel.DataAnnotations;
using backend.Models;

namespace backend.DTOs.Compartment
{
    public class UpdateCompartmentDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; } = string.Empty;

         //description
        public string Description { get; set; } = string.Empty;
         // location
        public CompartmentLocation Location { get; set; }

    }
}