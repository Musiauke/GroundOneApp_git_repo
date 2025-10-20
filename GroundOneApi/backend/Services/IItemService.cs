// business logic layer interface
using backend.DTOs.Items; // <--- To jest kluczowe!

namespace backend.Services;

public interface IItemService
{
    // Pobiera listę wszystkich przedmiotów (skrócone dane)
    Task<List<ItemDto>> GetAllItemsAsync();

    // Pobiera szczegóły pojedynczego przedmiotu
    Task<ItemDetailsDto?> GetItemByIdAsync(int id);

    // Tworzy nowy przedmiot
    Task<ItemDto> CreateItemAsync(CreateItemDto createDto);

    // Aktualizuje istniejący przedmiot
    Task<ItemDto?> UpdateItemAsync(int id, UpdateItemDto updateDto);

    // Usuwa przedmiot
    Task<bool> DeleteItemAsync(int id);
}