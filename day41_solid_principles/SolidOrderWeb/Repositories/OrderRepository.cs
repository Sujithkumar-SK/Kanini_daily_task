public class OrderRepository : IOrderRepository
{
  private readonly List<OrderModel> _orders = new();
  public void SaveOrder(OrderModel order)
  {
    _orders.Add(order);
  }
  public List<OrderModel> GetAllOrders()
  {
    return _orders;
  }
}