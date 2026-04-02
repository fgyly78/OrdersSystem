namespace OrdersSystem.Requests.Ordrers
{
    public record AddItemOrderRequest(Guid ProductId, string ProductName, decimal UnitPrice, string Currency, int Quantity);

}
