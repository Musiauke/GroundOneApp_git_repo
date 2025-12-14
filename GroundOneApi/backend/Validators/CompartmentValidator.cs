using FluentValidation;
using backend.Models;

public class CompartmentValidator : AbstractValidator<Compartment>
{
    public CompartmentValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Nazwa przedziału jest wymagana")
            .MaximumLength(100).WithMessage("Nazwa nie może być dłuższa niż 100 znaków");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("Opis przedziału jest wymagany")
            .MaximumLength(500).WithMessage("Opis nie może być dłuższy niż 500 znaków");

        RuleFor(c => c.Location)
            .IsInEnum().WithMessage("Nieprawidłowa lokalizacja przedziału");

        RuleFor(c => c.VehicleId)
            .GreaterThan(0).WithMessage("ID pojazdu musi być większe niż 0");
    }
}