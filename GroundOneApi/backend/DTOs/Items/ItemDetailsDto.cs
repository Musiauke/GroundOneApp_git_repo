using backend.Models;

namespace backend.DTOs.Items;

public class ItemDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int YearOfManufacture { get; set; }
    public string? Notes { get; set; }
    public EquipmentCategory Category { get; set; }
    public int Quantity { get; set; }
    public ItemStatus Status { get; set; }
    public DateTime? LastInspection { get; set; }
    public DateTime? NextInspection { get; set; }
    public int? CompartmentId { get; set; }
    // think about nested info like to to vehicle and compartment
}