using backend.DTOs.Items;
using backend.Models;
using backend.Repository; 

namespace backend.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        // repository injection
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // 1. GET ALL
        public async Task<List<ItemDto>> GetAllItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync(); // dowload all items from repository
            
            // mapping to ItemDto (short data)
            return items.Select(i => new ItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Quantity = i.Quantity,
                Category = i.Category,
                Status = i.Status
            }).ToList();
        }

        // 2. GET ITEM BY ID 
        public async Task<ItemDetailsDto?> GetItemByIdAsync(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null)
                return null;

            // Mapowanie z Modelu Item na ItemDetailsDto (pełne dane)
            return new ItemDetailsDto
            {
                Id = item.Id,
                Name = item.Name,
                Manufacturer = item.Manufacturer,
                YearOfManufacture = item.YearOfManufacture,
                Category = item.Category,
                Quantity = item.Quantity,
                LastInspection = item.LastInspection,
                NextInspection = item.NextInspection,
                Status = item.Status,
                Notes = item.Notes,
                CompartmentId = item.CompartmentId
            };
        }

        // 3. CREATE ITEM
        public async Task<ItemDto> CreateItemAsync(CreateItemDto createDto)
        {
            // Mapowanie z CreateItemDto (wejście) na Model Item
            var item = new Item
            {
                Name = createDto.Name,
                Manufacturer = createDto.Manufacturer,
                YearOfManufacture = createDto.YearOfManufacture,
                Category = createDto.Category,
                Status = createDto.Status,
                Quantity = createDto.Quantity,
                LastInspection = createDto.LastInspection,
                NextInspection = createDto.NextInspection,
                Notes = createDto.Notes,
                CompartmentId = createDto.CompartmentId,
                
            };

            var createdItem = await _itemRepository.AddAsync(item);

            // Mapowanie powrotne z Modelu Item na ItemDto (skrócony rezultat)
            return new ItemDto
            {
                Id = createdItem.Id,
                Name = createdItem.Name,
                Quantity = createdItem.Quantity,
                Category = createdItem.Category,
                Status = createdItem.Status
            };
        }

        // 4. UPDATE ITEM
        public async Task<ItemDto?> UpdateItemAsync(int id, UpdateItemDto updateDto)
        {
            var existingItem = await _itemRepository.GetByIdAsync(id);
            if (existingItem == null)
                return null;

            // Update only if in DTO is not null
            if (updateDto.Name != null)
                existingItem.Name = updateDto.Name;

            if (updateDto.Manufacturer != null)
                existingItem.Manufacturer = updateDto.Manufacturer;

            if (updateDto.YearOfManufacture.HasValue)
                existingItem.YearOfManufacture = updateDto.YearOfManufacture.Value;

            if (updateDto.Quantity.HasValue)
                existingItem.Quantity = updateDto.Quantity.Value;

            if (updateDto.LastInspectionDate.HasValue)
                existingItem.LastInspection = updateDto.LastInspectionDate.Value;

            if (updateDto.NextInspectionDate.HasValue)
                existingItem.NextInspection = updateDto.NextInspectionDate.Value;

            if (updateDto.Notes != null)
                existingItem.Notes = updateDto.Notes;

            if (updateDto.CompartmentId.HasValue)
                existingItem.CompartmentId = updateDto.CompartmentId.Value;

            await _itemRepository.UpdateAsync(existingItem);

            // retrieving updated item
            return new ItemDto
            {
                Id = existingItem.Id,
                Name = existingItem.Name,
                Quantity = existingItem.Quantity,
                Category = existingItem.Category,
                Status = existingItem.Status
            };
        }
        // 5. DELETE ITEM
        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            if (item == null)
                return false;

            await _itemRepository.DeleteAsync(id);
            return true;
        }
    }
}