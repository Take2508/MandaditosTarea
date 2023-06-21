using MediatR;
using ErrorOr;
using Application.Products.Common;

namespace Application.Products;

public record CreateProductCommand(
    string Name,
    string Sku,
    string Currency,
    decimal Amount
) : IRequest<ErrorOr<ProductResponse>>;