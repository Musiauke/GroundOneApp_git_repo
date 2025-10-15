using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs
{
    public class VehicleDTO
    {
        // Id not send
        public string Name { get; set; }
        public string Type { get; set; }
        public string Cryptonym { get; set; }
        public string RegistrationNumber { get; set; }
        public int YearOfManufacture { get; set; }
        public DateTime LastInspection { get; set; }
        public DateTime NextInspection { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}