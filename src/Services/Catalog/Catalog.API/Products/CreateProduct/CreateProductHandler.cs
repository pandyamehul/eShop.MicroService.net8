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

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor( product => product.Name ).NotEmpty().WithMessage("Name is required");
        RuleFor(product => product.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(product => product.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(product => product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal class CreateProductHandler
    (IDocumentSession session, IValidator<CreateProductCommand> validator)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Business logic to be implemented below to create product

        // Business validation
        var result = await validator.ValidateAsync(command, cancellationToken);
        var error = result.Errors.Select(x => x.ErrorMessage).ToList();
        if (error.Any()) {
            throw new ValidationException(error.FirstOrDefault());
        }

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
