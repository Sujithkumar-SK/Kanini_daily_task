public class OnlineOrder : IOrder
{
    public string PlaceOrder(double amount)
    {
        return $"Online order placed for amount: {amount}";
    }
}
