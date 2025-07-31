public class OrderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public OrderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IOrder GetOrder(string orderType)
    {
        return orderType switch
        {
            "Online" => _serviceProvider.GetService<OnlineOrder>(),
            "Store"  => _serviceProvider.GetService<StoreOrder>(),
            _ => throw new ArgumentException("Invalid order type")
        };
    }
}
