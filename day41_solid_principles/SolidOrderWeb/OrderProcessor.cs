public class OrderProcessor
{
    private readonly IOrderRepository _repository;
    private readonly INotification _notification;

    public OrderProcessor(IOrderRepository repository, INotification notification)
    {
        _repository = repository;
        _notification = notification;
    }

    public List<string> ProcessOrder(IOrder order, double amount)
    {
        var logs = new List<string>
        {
            "Order processing started...",
            order.PlaceOrder(amount),
        };
        _repository.SaveOrder(new OrderModel
        {
            OrderType = order.GetType().Name,
            Amount = amount,
        });
        logs.Add("Order saved to repository.");
        logs.AddRange(_notification.Send());
        return logs;
    }
}
