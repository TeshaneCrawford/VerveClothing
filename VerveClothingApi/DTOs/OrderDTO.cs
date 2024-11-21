using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class CreateOrderDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public List<CreateOrderItemDto> OrderItems { get; set; }
        [Required]
        public int ShippingAddressId { get; set; }
    }

    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int VariantId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateOrderItemDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int VariantId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
