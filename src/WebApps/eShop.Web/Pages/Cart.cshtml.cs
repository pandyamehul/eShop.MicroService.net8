namespace AspnetRunBasics;

public class CartModel (
        IBasketService basketService,
        ILogger logger
    ) 
    : PageModel
{
    //private readonly ICartRepository _cartRepository;

    //public CartModel(ICartRepository cartRepository)
    //{
    //    _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
    //}

    //public Entities.Cart Cart { get; set; } = new Entities.Cart();
    public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

    public async Task<IActionResult> OnGetAsync()
    {
        //Cart = await _cartRepository.GetCartByUserName("test");
        Cart = await basketService.LoadUserBasket();
        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
    {
        //await _cartRepository.RemoveItem(cartId, cartItemId);
        logger.LogInformation("Removed to Cart clicked");
        Cart = await basketService.LoadUserBasket();
        Cart.Items.RemoveAll(x => x.ProductId == productId);
        await basketService.StoreBasket(new StoreBasketRequest(Cart));
        return RedirectToPage();
    }
}