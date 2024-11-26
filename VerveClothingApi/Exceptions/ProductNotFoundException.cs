namespace VerveClothingApi.Exceptions
{
    public class ProductNotFoundException(int productId) : KeyNotFoundException($"Product with ID {productId} not found")
    {
    }
}
