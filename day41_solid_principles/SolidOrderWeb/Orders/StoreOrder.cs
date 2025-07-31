public class StoreOrder : IOrder
{
    public string PlaceOrder(double amount)
    {
        return $"Store order placed for amount: {amount}";
    }
}
