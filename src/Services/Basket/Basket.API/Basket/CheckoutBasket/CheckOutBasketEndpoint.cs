namespace Basket.API.Basket.CheckoutBasket;

public record CheckOutBasketRequest(BasketCheckOutDto BasketCheckOutDto);

public record CheckOutBasketResponse(bool IsSuccess);

public class CheckOutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckOutBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<CheckOutBasketCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CheckOutBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("CheckoutBasket")
        .Produces<CheckOutBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Basket")
        .WithDescription("Checkout Basket");
    }
}
