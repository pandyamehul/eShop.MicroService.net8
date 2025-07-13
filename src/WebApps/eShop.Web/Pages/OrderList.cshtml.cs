namespace eShop.Web.Pages;

public class OrderListModel(
        IOrderService orderService,
        ILogger<OrderListModel> logger
    ) 
    : PageModel
{
    public IEnumerable<OrderModel> Orders { get; set; } = new List<OrderModel>();

    public async Task<IActionResult> OnGetAsync()
    {
        logger.LogInformation("Get Orders");
        var CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
        var response = await orderService.GetOrdersByCustomer(CustomerId);
        Orders = response.Orders;
        return Page();
    }


}
