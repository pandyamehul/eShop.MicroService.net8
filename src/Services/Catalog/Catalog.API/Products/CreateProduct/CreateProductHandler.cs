using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
        string Name,
        string Description,
        string ImageFile,
        decimal Price,
        List<String> Category
    ) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);
internal class CreateProductHandler(IDocumentSession session) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Business logic to be implemented below to create product

        // Create product entity from command object
        var product = new Product {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        // Save to Db
        session.Store( product );
        await session.SaveChangesAsync(cancellationToken);

        // Return create product result
        return new CreateProductResult(product.Id);

        //throw new NotImplementedException();
    }
};
