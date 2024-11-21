using VerveClothingApi.DTOs;

namespace VerveClothingApi.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderDto> GetByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<IEnumerable<OrderDto>> GetByUserIdAsync(int userId);
        Task<OrderDto> CreateAsync(CreateOrderDto createOrderDto);
        Task<OrderDto> UpdateStatusAsync(int id, string status);
        Task<bool> DeleteAsync(int id);
    }
}
