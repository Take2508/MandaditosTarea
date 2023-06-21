using Domain.Customers;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Customers.Update;
public record UpdateCustomerCommand(
    CustomerId CustomerId,
    string Name,
    string LastName,
    string Email,
    PhoneNumber PhoneNumber,
    string Country,
    string Line1,
    string Line2,
    string City,
    string State,
    string ZipCode
) : IRequest<ErrorOr<Unit>>;

