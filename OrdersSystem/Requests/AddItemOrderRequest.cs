namespace OrdersSystem.Requests
{
    public record AddItemOrderRequest(Guid ProductId, string ProductName, decimal UnitPrice, string Currency, int Quantity);

}
