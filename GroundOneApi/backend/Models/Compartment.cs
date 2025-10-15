using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Compartment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; //should i use enums like rear, roof, first commander, first driver etc.?
        public string Description { get; set; } = string.Empty;
        public CompartmentLocation Location { get; set; }

        // Relations

        public int VehicleId { get; set; } // Foreign key
        public Vehicle Vehicle { get; set; } = null!; // Navigation property MANY TO ONE

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}