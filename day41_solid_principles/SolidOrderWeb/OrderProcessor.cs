public class OrderProcessor
{
    private readonly IDatabase _database;
    private readonly INotification _notification;

    public OrderProcessor(IDatabase database, INotification notification)
    {
        _database = database;
        _notification = notification;
    }

    public List<string> ProcessOrder(IOrder order, double amount)
    {
        var logs = new List<string>
        {
            "Order processing started...",
            order.PlaceOrder(amount),
            _database.Save(),
            _notification.Send()
        };
        return logs;
    }
}
