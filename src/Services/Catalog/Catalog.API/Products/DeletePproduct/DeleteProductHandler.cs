using Catalog.API.Products.GetProduct;
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(product => product.Id).NotEmpty().WithMessage("Id is required");        
    }
}
internal class DeleteProductCommandHandler
    (
        IDocumentSession session
        //, ILogger<GetProductsQueryHandler> logger
    )
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        //logger.LogInformation("DeleteProductCommandHandler.Handle : called with {@Query}", command);
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}
