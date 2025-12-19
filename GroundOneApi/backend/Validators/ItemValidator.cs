using FluentValidation;
using backend.Models;

namespace backend.Validators;

/// <summary>
/// validator for model: Item
/// </summary>
public class ItemValidator : AbstractValidator<Item>
{
    public ItemValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("Name for item is required")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters");

        RuleFor(i => i.Manufacturer)
            .NotEmpty().WithMessage("Manufacturer is required")
            .MaximumLength(100).WithMessage("Manufacturer name cannot be longer than 100 characters");

        RuleFor(i => i.YearOfManufacture)
            .InclusiveBetween(1900, DateTime.Now.Year + 1)
            .WithMessage($"Year of manufacture must be between 1900 and {DateTime.Now.Year + 1}");

        RuleFor(i => i.Category)
            .IsInEnum().WithMessage("Invalid item category");

        RuleFor(i => i.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0")
            .LessThanOrEqualTo(1000).WithMessage("Quantity cannot exceed 1000");
        RuleFor(i => i.LastInspection)
            .LessThanOrEqualTo(DateTime.Now)
            .When(i => i.LastInspection.HasValue)
            .WithMessage("Last inspection date cannot be in the future");

        RuleFor(i => i.NextInspection)
            .GreaterThan(i => i.LastInspection)
            .When(i => i.NextInspection.HasValue && i.LastInspection.HasValue)
            .WithMessage("Next inspection date must be after last inspection date");

        RuleFor(i => i.Status)
            .IsInEnum().WithMessage("Invalid item status");

        RuleFor(i => i.Notes)
            .MaximumLength(500).WithMessage("Notes cannot be longer than 500 characters")
            .When(i => !string.IsNullOrEmpty(i.Notes));
    }
}