using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
            string Name,
            string Description,
            string ImageFile,
            decimal Price,
            List<String> Categpry
        ) : IRequest<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    internal class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //Business logic to be implemented below to create product

            throw new NotImplementedException();
        }
    }
}
