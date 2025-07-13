namespace eShop.Web.Pages;

public class IndexModel 
    (ICatalogService catalogService, ILogger<IndexModel> logger)
    : PageModel
{
    public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();

    public async Task<IActionResult> OnGetAsync()
    {
        logger.LogInformation("Index Page Visited");
        var result = await catalogService.GetProducts();
        ProductList = result.Products;
        return Page();
    }
}
