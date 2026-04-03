namespace OrdersSystem.Requests.Ordrers
{
    public record AddItemOrderRequest(Guid ProductId, int Quantity);

}
