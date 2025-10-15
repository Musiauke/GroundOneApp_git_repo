using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // eg. Halligan
        public string Manufacturer { get; set; } = string.Empty;
        public int YearOfManufacture { get; set; } = DateTime.Now.Year;
        public EquipmentCategory Category { get; set; }
        // category, needs enum
        public int Quantity { get; set; } = 1;
        public DateTime? LastInspection { get; set; } = null;
        public DateTime? NextInspection { get; set; } = null;
        public ItemStatus Status { get; set; } = ItemStatus.Available;
        // is in available, needs enum 
        public string? Notes { get; set; } = string.Empty; // decide between notes and description or both

        //  Foreign key to Compartment, why it needs and other dont?
        public int? CompartmentId { get; set; }
        public Compartment? Compartment { get; set; }
        // compartment, needs enums
    }
}