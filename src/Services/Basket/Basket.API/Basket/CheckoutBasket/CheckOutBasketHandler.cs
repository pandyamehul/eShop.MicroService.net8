using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckOutBasketCommand(BasketCheckOutDto BasketCheckOutDto)
    : ICommand<CheckOutBasketResult>;
public record CheckOutBasketResult(bool IsSuccess);

public class CheckOutBasketCommandValidator : AbstractValidator<CheckOutBasketCommand>
{
    public CheckOutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckOutDto).NotNull().WithMessage("Basket Checkout Dto can't be null");
        RuleFor(x => x.BasketCheckOutDto.UserName).NotEmpty().WithMessage("User Name is required");
    }
}

public class CheckoutBasketCommandHandler
    (IBasketRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckOutBasketCommand, CheckOutBasketResult>
{
    public async Task<CheckOutBasketResult> Handle(CheckOutBasketCommand command, CancellationToken cancellationToken)
    {
        // get existing basket with total price
        // Set totalprice on basketcheckout event message
        // send basket checkout event to rabbitmq using masstransit
        // delete the basket

        // Get basket details for user from db
        var basket = await repository.GetBasket(command.BasketCheckOutDto.UserName, cancellationToken);
        
        // return false if basket for user is blank
        if (basket == null)
        {
            return new CheckOutBasketResult(false);
        }
        
        // prepare event message
        var eventMessage = command.BasketCheckOutDto.Adapt<BasketCheckOutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        // publish event message
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        //delete basket after publish event
        await repository.DeleteBasket(command.BasketCheckOutDto.UserName, cancellationToken);

        // return checkout result
        return new CheckOutBasketResult(true);
    }
}