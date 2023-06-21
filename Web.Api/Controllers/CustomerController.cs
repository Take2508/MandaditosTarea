using Application.Customers.Create;
using Application.Customers.Update;
using Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[Route("Customer")]
public class Customers : ApiController
{
    private readonly ISender _mediator;

    public Customers(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var createCustomerResult = await _mediator.Send(command);

        return createCustomerResult.Match(
            customer => Ok(),
            errors => Problem(errors)
        );
    }
    [HttpPut("{customerId}")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand command)
    {
        var updateCustomerResult = await _mediator.Send(command);

        return updateCustomerResult.Match(
            customer => Ok(),
            errors => Problem(errors)
        );
    }

}