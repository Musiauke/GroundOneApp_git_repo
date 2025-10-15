namespace backend.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // e.g., Fire Truck, Ambulance
        public string Cryptonym { get; set; } = string.Empty; // e.g. 451-25, 457-01
        public string RegistrationNumber { get; set; } = string.Empty;
        public int YearOfManufacture { get; set; }
        public DateTime? LastInspection { get; set; }
        public DateTime? NextInspection { get; set; }
        public VehicleStatus Status { get; set; } = VehicleStatus.Available; // e.g., Available, On Action, Under Maintenance
        public string? Notes { get; set; }
        public ICollection<Compartment> Compartments { get; set; } = new List<Compartment>(); // Navigation property ONE TO MANY
    }
}