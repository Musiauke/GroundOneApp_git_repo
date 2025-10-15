using backend.DTOs.Vehicles;
using backend.Models;
using backend.Repository;
// implementation of menu , the recipe 


// it is written based on IVehicleService


// but it uses IVehicleRepository to get data from database
namespace backend.Services
{
    public class VehicleService : IVehicleService
    // implementation of its own interface IVehicleService
    {

        // says that it will use IVehicleRepository to get data
        private readonly IVehicleRepository _vehicleRepository;

        // Dependency Injection (it gets repository thanks to constructor injection)
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<List<VehicleResponseDto>> GetAllVehicleAsyncs()
        {
            // take it from repository
            var vehicles = await _vehicleRepository.GetAllAsyncs();
            // map it to DTO
            return vehicles.Select(v = new VehicleResponseDto
            {
                Id = v.Id,
                Brand = v.Brand,
                Model = v.Model,
                Year = v.Year,
                LicensePlate = v.LicensePlate,
                PricePerDay = v.PricePerDay,
                IsAvailable = v.IsAvailable
            }).ToList();
            // that's creating  new object DTO in memory, to nicely pack data from the entity
        }


        public async Task<VehicleDetailsDto?> GetVehicleByIdAsync(int id);
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            if (vehicle == null)
                return null;

        }

        public async Task<VehicleDetailsDto?> CreateVehicleAsync(int id);
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            if (vehicle == null)
                return null;

        }

        public async Task<VehicleDetailsDto?> UpdateVehicleAsync(int id);
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            if (vehicle == null)
                return null;

        }


public async Task<VehicleDetailsDto?> DeleteVehicleAsync(int id);
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            if (vehicle == null)
                return null;

        }


}
    }
}

