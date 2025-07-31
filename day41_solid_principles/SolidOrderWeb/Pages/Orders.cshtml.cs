using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class OrdersModel : PageModel
{
    [BindProperty]
    public OrderModel Order { get; set; }

    public List<string> Logs { get; set; }

    private readonly OrderFactory _orderFactory;
    private readonly OrderProcessor _processor;

    public OrdersModel(OrderFactory orderFactory, OrderProcessor processor)
    {
        _orderFactory = orderFactory;
        _processor = processor;
    }

    public void OnPost()
    {
        var orderService = _orderFactory.GetOrder(Order.OrderType);
        Logs = _processor.ProcessOrder(orderService, Order.Amount);
    }
}
