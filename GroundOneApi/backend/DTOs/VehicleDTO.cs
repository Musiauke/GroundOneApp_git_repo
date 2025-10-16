using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs
{
    public class VehicleDTO
    {
        // Id not send
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Cryptonym { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public int YearOfManufacture { get; set; }
        public DateTime LastInspection { get; set; }
        public DateTime NextInspection { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}