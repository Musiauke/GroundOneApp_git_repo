using FluentValidation;
using backend.Models;
using backend.DTOs.Vehicles;

namespace backend.Validators;

/// <summary>
///  validator for Vehicle
/// </summary>
public class VehicleValidator : AbstractValidator<Vehicle>
{
    public VehicleValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters")
            .MinimumLength(2).WithMessage("Name must have at least 2 characters");

        RuleFor(v => v.Type)
            .NotEmpty().WithMessage("Vehicle type is required")
            .MaximumLength(50).WithMessage("Vehicle type cannot be longer than 50 characters");

        RuleFor(v => v.Cryptonym)
            .NotEmpty().WithMessage("Cryptonym is required")
            .MaximumLength(20).WithMessage("Cryptonym cannot be longer than 20 characters")
            .Matches(@"^\d{3}-\d{2}$")
            .WithMessage("Cryptonym must be in the format XXX-XX (e.g., 451-25)");

        RuleFor(v => v.RegistrationNumber)
            .NotEmpty().WithMessage("Registration number is required")
            .MaximumLength(20).WithMessage("Registration number cannot be longer than 20 characters")
            .Matches(@"^[A-Z0-9\s-]+$")
            .WithMessage("Invalid registration number format");

        RuleFor(v => v.YearOfManufacture)
            .InclusiveBetween(1900, DateTime.Now.Year + 1)
            .WithMessage($"Year of manufacture must be between 1900 and {DateTime.Now.Year + 1}");
        RuleFor(v => v.LastInspection)
            .LessThanOrEqualTo(DateTime.Now)
            .When(v => v.LastInspection.HasValue)
            .WithMessage("Last inspection date cannot be in the future");

        RuleFor(v => v.NextInspection)
            .GreaterThan(v => v.LastInspection)
            .When(v => v.NextInspection.HasValue && v.LastInspection.HasValue)
            .WithMessage("Next inspection date must be after last inspection date");

        RuleFor(v => v.Status)
            .IsInEnum().WithMessage("Invalid vehicle status");

        RuleFor(v => v.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot be longer than 1000 characters")
            .When(v => !string.IsNullOrEmpty(v.Notes));
    }
}

/// <summary>
/// validator for CreateVehicleDto
/// </summary>
public class CreateVehicleDtoValidator : AbstractValidator<CreateVehicleDto>
{
    public CreateVehicleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Vehicle name is required")
            .MaximumLength(100);

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Vehicle type is required")
            .MaximumLength(50);

        RuleFor(x => x.Cryptonym)
            .NotEmpty().WithMessage("Cryptonym is required")
            .Matches(@"^\d{3}-\d{2}$")
            .WithMessage("Cryptonym must be in the format XXX-XX");

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty().WithMessage("Registration number is required")
            .Matches(@"^[A-Z0-9\s-]+$");

        RuleFor(x => x.YearOfManufacture)
            .InclusiveBetween(1900, DateTime.Now.Year + 1);
    }
}

/// <summary>
/// validator for UpdateVehicleDto
/// </summary>
public class UpdateVehicleDtoValidator : AbstractValidator<UpdateVehicleDto>
{
    public UpdateVehicleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Type)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Cryptonym)
            .NotEmpty()
            .Matches(@"^\d{3}-\d{2}$");

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .Matches(@"^[A-Z0-9\s-]+$");

        RuleFor(x => x.YearOfManufacture)
            .InclusiveBetween(1900, DateTime.Now.Year + 1);

        RuleFor(x => x.Status)
            .IsInEnum();
    }
}