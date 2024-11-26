namespace VerveClothingApi.Exceptions
{
    public class InsufficientStockException(int productId, int requestedQuantity, int availableQuantity) : Exception($"Insufficient stock for product {productId}. Requested quantity: {requestedQuantity}. Available quantity: {availableQuantity}.")
    {
    }
}
