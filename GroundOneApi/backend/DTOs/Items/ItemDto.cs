namespace backend.DTOs.Tiems;

public class ItemSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Notes { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? LastInspectionDate { get; set; }
    public DateTime? NextInspectionDate { get; set; }
    // Compartment info
    public int? CompartmentId { get; set; } // it needs to be mapped like this CompartmentName = item.Compartment?.Name if mapping manually
    public string? CompartmentName { get; set; }
}