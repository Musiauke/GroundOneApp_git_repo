using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs.Vehicles
{
    public class VehicleDetailsDto
    {
        // basic info
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Cryptonym { get; set; }
        public string RegistrationNumber { get; set; }
        public int YearOfManufacture { get; set; }

        // statuses and dates
        public string Status { get; set; }
        public DateTime? LastInspection { get; set; }
        public DateTime? NextInspection { get; set; }
        public string? Notes { get; set; }
    }

    // Nested DTO for compartments
    // learn it
    // it looks like this: 
    
    
    //     {
    //     public int Id { get; set; }
    //     public string Name { get; set; }
    //     public string Location { get; set; }
    //     public List<ItemSummaryDto> Items { get; set; } = new();
    // }

    // // Zagnieżdżone DTO dla itemów
    // public class ItemSummaryDto
    // {
    //     public int Id { get; set; }
    //     public string Name { get; set; }
    //     public int Quantity { get; set; }
    //     public string Status { get; set; }
    // }

}