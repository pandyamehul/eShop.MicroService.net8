using BuildingBlocks.CQRS;

namespace Order.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Create order entity from command object
        // save entity to database
        // return the result
        throw new NotImplementedException();
    }
}
