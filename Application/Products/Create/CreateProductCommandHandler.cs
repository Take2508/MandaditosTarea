using Application.Products.Common;
using Domain.Primitives;
using Domain.Products;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Products;
internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<ProductResponse>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product(
            new ProductId(Guid.NewGuid()), Sku.Create(command.Sku)!, command.Name, new Money(command.Currency, command.Amount));

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProductResponse();
    }
}