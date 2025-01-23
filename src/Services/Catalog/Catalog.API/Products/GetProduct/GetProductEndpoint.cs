using Catalog.API.Models;
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProduct;

public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var response = result.Adapt<GetProductResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProduct")
        .Produces<GetProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product")
        .WithDescription("Get Product");
    }
}
