using backend.Models;

namespace backend.DTOs.Items;

public class UpdateItemDto
{
    public string? Name { get; set; }
    public string? Manufacturer { get; set; }
    public int? YearOfManufacture { get; set; }
    public string? Notes { get; set; }
    public EquipmentCategory? Category { get; set; }
    public int? Quantity { get; set; }
    public ItemStatus? Status { get; set; }
    public DateTime? LastInspectionDate { get; set; }
    public DateTime? NextInspectionDate { get; set; }
    public int? CompartmentId { get; set; }
}