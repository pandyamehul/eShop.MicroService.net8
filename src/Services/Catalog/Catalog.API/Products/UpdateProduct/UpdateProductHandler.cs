
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProduct;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
        Guid Id,
        string Name,
        string Description,
        string ImageFile,
        decimal Price,
        List<string> Category
    )
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(product => product.Id).NotEmpty().WithMessage("Name is required");
        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2,150).WithMessage("Name must be between 2 to 150 character length");
        RuleFor(product => product.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(product => product.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(product => product.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
internal class UpdateProductCommandHandler
    (
        IDocumentSession session
        //, ILogger<GetProductsQueryHandler> logger
    )
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        //logger.LogInformation("UpdateProductCommandHandler.Handle : called with {@Query}", command);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        product.Name = command.Name;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        product.Category = command.Category;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
