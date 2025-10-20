using backend.Models;

namespace backend.DTOs.Items;

public class CreateItemDto
{
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int YearOfManufacture { get; set; } = DateTime.Now.Year;
    public string? Notes { get; set; }
    public EquipmentCategory Category { get; set; }
    public int Quantity { get; set; } = 1;
    public ItemStatus Status { get; set; }
    public DateTime? LastInspection { get; set; }
    public DateTime? NextInspection { get; set; }
    public int? CompartmentId { get; set; }
}