using VerveClothingApi.DTOs;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Services
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<OrderDto> UpdateOrderStatusAsync(int id, string status);
        Task<bool> DeleteOrderAsync(int id);
    }

    public class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId)
        {
            return await _orderRepository.GetByUserIdAsync(userId);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            // Todo. Add additional business logic here, e.g., inventory checks
            return await _orderRepository.CreateAsync(createOrderDto);
        }

        public async Task<OrderDto> UpdateOrderStatusAsync(int id, string status)
        {
            // Todo. Add additional business logic here, e.g., sending notifications
            return await _orderRepository.UpdateStatusAsync(id, status);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id);
        }
    }
}
