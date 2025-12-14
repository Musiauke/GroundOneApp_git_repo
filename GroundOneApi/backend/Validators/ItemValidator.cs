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
            .NotEmpty().WithMessage("Nazwa przedmiotu jest wymagana")
            .MaximumLength(100).WithMessage("Nazwa nie może być dłuższa niż 100 znaków");

        RuleFor(i => i.Manufacturer)
            .NotEmpty().WithMessage("Producent jest wymagany")
            .MaximumLength(100).WithMessage("Nazwa producenta nie może być dłuższa niż 100 znaków");

        RuleFor(i => i.YearOfManufacture)
            .InclusiveBetween(1900, DateTime.Now.Year + 1)
            .WithMessage($"Rok produkcji musi być między 1900 a {DateTime.Now.Year + 1}");

        RuleFor(i => i.Category)
            .IsInEnum().WithMessage("Nieprawidłowa kategoria sprzętu");

        RuleFor(i => i.Quantity)
            .GreaterThan(0).WithMessage("Ilość musi być większa niż 0")
            .LessThanOrEqualTo(1000).WithMessage("Ilość nie może przekraczać 1000");

        RuleFor(i => i.LastInspection)
            .LessThanOrEqualTo(DateTime.Now)
            .When(i => i.LastInspection.HasValue)
            .WithMessage("Data ostatniego przeglądu nie może być w przyszłości");

        RuleFor(i => i.NextInspection)
            .GreaterThan(i => i.LastInspection)
            .When(i => i.NextInspection.HasValue && i.LastInspection.HasValue)
            .WithMessage("Data następnego przeglądu musi być po ostatnim przeglądzie");

        RuleFor(i => i.Status)
            .IsInEnum().WithMessage("Nieprawidłowy status przedmiotu");

        RuleFor(i => i.Notes)
            .MaximumLength(500).WithMessage("Notatki nie mogą być dłuższe niż 500 znaków")
            .When(i => !string.IsNullOrEmpty(i.Notes));
    }
}