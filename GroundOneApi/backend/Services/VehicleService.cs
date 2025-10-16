using backend.DTOs.Vehicles;
using backend.Models;
using backend.Repository;

namespace backend.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<List<VehicleResponseDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            return vehicles.Select(v => new VehicleResponseDto
            {
                Id = v.Id,
                Name = v.Name,
                Type = v.Type,
                Cryptonym = v.Cryptonym,
                RegistrationNumber = v.RegistrationNumber,
                YearOfManufacture = v.YearOfManufacture,
                Status = v.Status.ToString(),
                NextInspection = v.NextInspection
            }).ToList();
        }

        public async Task<VehicleDetailsDto?> GetVehicleByIdAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            if (vehicle == null)
                return null;

            // mapping to VehicleDetailsDto
            
            return new VehicleDetailsDto
            {
                Id = vehicle.Id,
                Name = vehicle.Name,
                Type = vehicle.Type,
                Cryptonym = vehicle.Cryptonym,
                RegistrationNumber = vehicle.RegistrationNumber,
                YearOfManufacture = vehicle.YearOfManufacture,
                Status = vehicle.Status.ToString(),
                NextInspection = vehicle.NextInspection
                // add accordingly as in VehicleDetailsDto
            };
        }

    public async Task<VehicleResponseDto> CreateVehicleAsync(CreateVehicleDto createDto)
    {
        var vehicle = new Vehicle
        {
            Name = createDto.Name,
            Type = createDto.Type,
            Cryptonym = createDto.Cryptonym,
            RegistrationNumber = createDto.RegistrationNumber,
            YearOfManufacture = createDto.YearOfManufacture,
            LastInspection = createDto.LastInspection,
            NextInspection = createDto.NextInspection,
            Notes = createDto.Notes
        };

        var createdVehicle = await _vehicleRepository.AddAsync(vehicle);

        return new VehicleResponseDto
        {
            Id = createdVehicle.Id,
            Name = createdVehicle.Name,
            Type = createdVehicle.Type,
            Cryptonym = createdVehicle.Cryptonym,
            RegistrationNumber = createdVehicle.RegistrationNumber,
            YearOfManufacture = createdVehicle.YearOfManufacture,
            Status = createdVehicle.Status.ToString(),
            NextInspection = createdVehicle.NextInspection,
            LastInspection = createdVehicle.LastInspection,
            Notes = createdVehicle.Notes
        };
    }

        public async Task<VehicleResponseDto?> UpdateVehicleAsync(int id, UpdateVehicleDto updateDto)
        {
            var existingVehicle = await _vehicleRepository.GetByIdAsync(id);

            if (existingVehicle == null)
                return null;

            // Aktualizacja właściwości
            existingVehicle.Name = updateDto.Name;
            existingVehicle.Type = updateDto.Type;
            existingVehicle.Cryptonym = updateDto.Cryptonym;
            existingVehicle.RegistrationNumber = updateDto.RegistrationNumber;
            existingVehicle.YearOfManufacture = updateDto.YearOfManufacture;
            existingVehicle.NextInspection = updateDto.NextInspection;
            // hard one
            existingVehicle.Status = Enum.TryParse<VehicleStatus>(updateDto.Status, true, out var parsedStatus)
                ? parsedStatus
                : existingVehicle.Status;

            await _vehicleRepository.UpdateAsync(existingVehicle);

            return new VehicleResponseDto
            {
                Id = existingVehicle.Id,
                Name = existingVehicle.Name,
                Type = existingVehicle.Type,
                Cryptonym = existingVehicle.Cryptonym,
                RegistrationNumber = existingVehicle.RegistrationNumber,
                YearOfManufacture = existingVehicle.YearOfManufacture,
                Status = existingVehicle.Status.ToString(),
                NextInspection = existingVehicle.NextInspection
            };
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            if (vehicle == null)
                return false;

            await _vehicleRepository.DeleteAsync(id);
            return true;
        }
    }
}