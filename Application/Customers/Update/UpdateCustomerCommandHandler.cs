using MediatR;
using Domain.Customers;
using ErrorOr;
using Domain.ValueObjects;

namespace Application.Customers.Update
{
    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ErrorOr<Unit>>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var customerId = new CustomerId(Guid.Parse(command.CustomerId));
                var customer = await _customerRepository.GetByIdAsync(customerId);

                if (customer is null)
                {
                    return Error.Validation("Customer", "Customer not found");
                }

                // Actualizar los campos del cliente con los valores proporcionados en el comando
                customer.Name = command.Name;
                customer.LastName = command.LastName;
                customer.Email = command.Email;
                customer.PhoneNumber = command.PhoneNumber;


                // Actualiza otros campos según sea necesario

                await _customerRepository.Update(customer);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Error.Failure("UpdateCustomer.Failure", ex.Message);
            }
        }
    }
}
