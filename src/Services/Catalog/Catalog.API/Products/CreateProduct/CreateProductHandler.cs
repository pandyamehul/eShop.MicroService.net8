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
internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
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


        // Return create product result
        return new CreateProductResult(Guid.NewGuid());

        //throw new NotImplementedException();
    }
};
