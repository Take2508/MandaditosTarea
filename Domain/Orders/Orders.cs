using Domain.Customers;
using Domain.Primitives;
using Domain.Products;
using Domain.ValueObjects;

namespace Domain.Orders;

public sealed class Order : AgregateRoot
{
    private readonly List<LineItem> _lineItems = new();
    private Order()
    {

    }

    public OrderId Id { get; private set; }

    public CustomerId CustomerId { get; set; }

    public IReadOnlyList<LineItem> LineItems => _lineItems.AsReadOnly();

    public static Order Create(CustomerId customerId)
    {
        var order = new Order
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId
        };

        return order;
    }

    public void Add(ProductId productId, decimal quantity, Money price)
    {
        var LineItem = new LineItem(new LineItemId(Guid.NewGuid()), Id, productId, quantity, price);

        _lineItems.Add(LineItem);
    }

    public void RemoveLineItem(LineItemId lineItemId, IOrderRepository orderRepository)
    {
        if (orderRepository.HasOneLineItem(this))
        {
            return;
        }

        var lineItem = _lineItems.FirstOrDefault(li => li.Id == lineItemId);

        if (lineItem is null)
        {
            return;
        }

        _lineItems.Remove(lineItem);
    }
}