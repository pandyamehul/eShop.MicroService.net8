using BuildingBlocks.CQRS;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
            string Name,
            string Description,
            string ImageFile,
            decimal Price,
            List<String> Categpry
        ) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Business logic to be implemented below to create product
            // Create product entity from command object

            throw new NotImplementedException();
        }
    }
}
