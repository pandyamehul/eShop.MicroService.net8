﻿namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName):ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator :
    AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(basket => basket.UserName).NotNull().WithMessage("Cart cannot be null");
    }
}

public class DeleteBasketHandler
    (IBasketRepository basketRepository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        bool response = await basketRepository.DeleteBasket(command.UserName, cancellationToken);
        return new DeleteBasketResult(response);
    }
}
