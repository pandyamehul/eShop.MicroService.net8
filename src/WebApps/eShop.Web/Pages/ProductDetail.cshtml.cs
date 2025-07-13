namespace eShop.Web.Pages;

public class ProductDetailModel (
        IBasketService basketService,
        ICatalogService catalogService,
        ILogger<ProductDetailModel> logger
    )
    : PageModel
{
    [BindProperty]
    public string Color { get; set; }

    [BindProperty]
    public int Quantity { get; set; }
    public ProductModel Product { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(Guid productId)
    {
        var response = await catalogService.GetProduct(productId);
        Product = response.Product;
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        logger.LogInformation("Add to cart button clicked");
        var productResponse = await catalogService.GetProduct(productId);

        var basket = await basketService.LoadUserBasket();

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Price = productResponse.Product.Price,
            Quantity = Quantity,
            Color = Color
        });

        await basketService.StoreBasket(new StoreBasketRequest(basket));

        return RedirectToPage("Cart");
    }
}
