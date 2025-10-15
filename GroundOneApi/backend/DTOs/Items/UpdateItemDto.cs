public class UpdateItemDto
{
    public string? Name { get; set; }
    public string? Notes { get; set; }
    public string? Category { get; set; }
    public int? Quantity { get; set; }
    public string? Status { get; set; }
    public DateTime? LastInspectionDate { get; set; }
    public DateTime? NextInspectionDate { get; set; }
    public int? CompartmentId { get; set; }
}