public class CreateItemDto
{
    public string Name { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string Category { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public string Status { get; set; } = "Available";
    public DateTime? LastInspectionDate { get; set; }
    public DateTime? NextInspectionDate { get; set; }
    public int? CompartmentId { get; set; }
}