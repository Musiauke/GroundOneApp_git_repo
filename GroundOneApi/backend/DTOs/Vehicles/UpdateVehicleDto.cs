using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Vehicles
{
    public class UpdateVehicleDto
    {
        // validation
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type is required")]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cryptonym is required")]
        [StringLength(50)]
        public string Cryptonym { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Registration number is required")]
        [StringLength(20)]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Range(1900, 2100, ErrorMessage = "Production date must be from between 1900 and 2100")]
        public int YearOfManufacture { get; set; }
        public DateTime? LastInspection { get; set; }
        public DateTime? NextInspection { get; set; }

        [Required]
        public string Status { get; set; } = "Available";
        // not public VehicleStatus Status { get; set; } = VehicleStatus.Available; 
        // bcs its enum
        public string? Notes { get; set; }
        public List<int>? CompartmentIds { get; set; } 
        // not 
        // public ICollection<Compartment> Compartments { get; set; } = new List<Compartment>(); 
        // bcs it is meant to be lightweight DTO, not full entity    

    }
}