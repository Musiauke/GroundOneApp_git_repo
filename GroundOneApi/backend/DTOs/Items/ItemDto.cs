using backend.Models;

namespace backend.DTOs.Items;

public class ItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Notes { get; set; } = string.Empty;
    public EquipmentCategory Category { get; set; }
    public int Quantity { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.Available;
    public DateTime? LastInspectionDate { get; set; }
    public DateTime? NextInspectionDate { get; set; }
    // Compartment info
    public int? CompartmentId { get; set; } // it needs to be mapped like this CompartmentName = item.Compartment?.Name if mapping manually
    public string? CompartmentName { get; set; }
}