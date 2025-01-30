using Catalog.API.Products.GetProduct;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryQueryHandler
    (IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult>
        Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryQueryHandler.Handle : called with {@Query}", query);
        var products = await session.Query<Product>()
            .Where( p => p.Category.Contains(query.Category))
            .ToListAsync();
        if (products == null) { throw new ProductNotFoundException(); }
        return new GetProductByCategoryResult(products);
    }
}