public interface IOrderRepository
{
    void SaveOrder(OrderModel order);
    List<OrderModel> GetAllOrders();
}
