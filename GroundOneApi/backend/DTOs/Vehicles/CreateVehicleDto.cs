using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Vehicles
{
    public class CreateVehicleDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type is required")]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty; // GBA, GCBA, SRt

        [Required(ErrorMessage = "Cryptonym is required")]
        [StringLength(20)]
        [RegularExpression(@"^\d{3}-\d{2}$", ErrorMessage = "Cryptonym must be in the format XXX-XX (e.g. 451-25)")]
        public string Cryptonym { get; set; } = string.Empty;

        [Required(ErrorMessage = "Registration number is required")]
        [StringLength(20)]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year of manufacture is required")]
        [Range(1900, 2100, ErrorMessage = "Year of manufacture must be between 1900 and 2100")]
        public int YearOfManufacture { get; set; }

        // Optional on creation
        public DateTime? LastInspection { get; set; }

        public DateTime? NextInspection { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot be longer than 500 characters")]
        public string? Notes { get; set; }

        // Optional - you can assign the vehicle to compartments right away
        public List<int>? CompartmentIds { get; set; }

        // Status will default to "Available" in the backend - no need in DTO
    }
}