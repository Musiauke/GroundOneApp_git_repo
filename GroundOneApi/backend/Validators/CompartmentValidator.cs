using FluentValidation;
using backend.Models;

public class CompartmentValidator : AbstractValidator<Compartment>
{
    public CompartmentValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name for compartment is required")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("Description for compartment is required")
            .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters");
        RuleFor(c => c.Location)
            .IsInEnum().WithMessage("Invalid compartment location");

        RuleFor(c => c.VehicleId)
            .GreaterThan(0).WithMessage("Vehicle ID must be greater than 0");
    }
}