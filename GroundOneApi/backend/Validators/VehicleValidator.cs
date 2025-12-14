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
            .NotEmpty().WithMessage("Nazwa pojazdu jest wymagana")
            .MaximumLength(100).WithMessage("Nazwa nie może być dłuższa niż 100 znaków")
            .MinimumLength(2).WithMessage("Nazwa musi mieć minimum 2 znaki");

        RuleFor(v => v.Type)
            .NotEmpty().WithMessage("Typ pojazdu jest wymagany")
            .MaximumLength(50).WithMessage("Typ nie może być dłuższy niż 50 znaków");

        RuleFor(v => v.Cryptonym)
            .NotEmpty().WithMessage("Kryptonim jest wymagany")
            .MaximumLength(20).WithMessage("Kryptonim nie może być dłuższy niż 20 znaków")
            .Matches(@"^\d{3}-\d{2}$")
            .WithMessage("Kryptonim musi być w formacie XXX-XX (np. 451-25)");

        RuleFor(v => v.RegistrationNumber)
            .NotEmpty().WithMessage("Numer rejestracyjny jest wymagany")
            .MaximumLength(20).WithMessage("Numer rejestracyjny nie może być dłuższy niż 20 znaków")
            .Matches(@"^[A-Z0-9\s-]+$")
            .WithMessage("Nieprawidłowy format numeru rejestracyjnego");

        RuleFor(v => v.YearOfManufacture)
            .InclusiveBetween(1900, DateTime.Now.Year + 1)
            .WithMessage($"Rok produkcji musi być między 1900 a {DateTime.Now.Year + 1}");

        RuleFor(v => v.LastInspection)
            .LessThanOrEqualTo(DateTime.Now)
            .When(v => v.LastInspection.HasValue)
            .WithMessage("Data ostatniego przeglądu nie może być w przyszłości");

        RuleFor(v => v.NextInspection)
            .GreaterThan(v => v.LastInspection)
            .When(v => v.NextInspection.HasValue && v.LastInspection.HasValue)
            .WithMessage("Data następnego przeglądu musi być po ostatnim przeglądzie");

        RuleFor(v => v.Status)
            .IsInEnum().WithMessage("Nieprawidłowy status pojazdu");

        RuleFor(v => v.Notes)
            .MaximumLength(1000).WithMessage("Notatki nie mogą być dłuższe niż 1000 znaków")
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
            .NotEmpty().WithMessage("Nazwa pojazdu jest wymagana")
            .MaximumLength(100);

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Typ pojazdu jest wymagany")
            .MaximumLength(50);

        RuleFor(x => x.Cryptonym)
            .NotEmpty().WithMessage("Kryptonim jest wymagany")
            .Matches(@"^\d{3}-\d{2}$")
            .WithMessage("Kryptonim musi być w formacie XXX-XX");

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty().WithMessage("Numer rejestracyjny jest wymagany")
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