using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(basket => basket.Cart).NotNull().WithMessage("Cart cannot be null");
        RuleFor(basket => basket.Cart.UserName).NotEmpty().WithMessage("User Name is required");
        RuleFor(basket => basket.Cart.TotalPrice).GreaterThan(0).WithMessage("Price should be more than 0");
    }
}

public class StoreBasketCommandHandler
    (IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        //ShoppingCart cart = command.Cart;
        await DeductDiscount(command.Cart, cancellationToken);

        //TODO: Store data in db
        await basketRepository.StoreBasket(command.Cart, cancellationToken);
        //TODO: Update cache

        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        //Communicate with Discount.Grps and calculate latest price of product in shopping cart
        foreach (var item in cart.Items)
        {
            var discount = await discountProtoService.GetDiscountAsync(
                new GetDiscountRequest
                { ProductName = item.ProductName },
                cancellationToken: cancellationToken
            );
            item.Price -= discount.Amount;
        }
    }
}
