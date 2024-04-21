namespace Catalog.API.Products.CreateProduct;

public record CreateRecordRequest(
        string Name,
        string Description,
        string InageFile,
        decimal Price,
        List<string> Category
    );

public record CreateProductResponse(Guid Id);
public class CreateProductEndpoint
{
}
