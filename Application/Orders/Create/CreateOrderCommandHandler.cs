using Application.Orders.Common;
using Domain.Customers;
using Domain.Orders;
using Domain.Primitives;
using Domain.Products;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Orders.Create;
public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<OrderResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitofwork;
    public CreateOrderCommandHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository, IUnitOfWork unitofwork)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _unitofwork = unitofwork;
    }


    public async Task<ErrorOr<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

        if (customer is null)
        {
            return Error.NotFound("Customer.NotFound", $"The customer {request.CustomerId} does not exist");
        }

        var order = Order.Create(customer.Id);

        if (!request.Items.Any())
        {
            return Error.Conflict("Order.Detail", "For create at order you need to specify the details of the order");
        }

        foreach (var item in request.Items)
        {
            order.Add(new ProductId(item.ProductId), item.Quantity, new Money("$", item.Price));
        }
        _orderRepository.Add(order);

        await _unitofwork.SaveChangesAsync(cancellationToken);

        return new OrderResponse();
    }
}