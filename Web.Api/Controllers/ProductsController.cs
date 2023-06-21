using Application.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using Web.Api.Controllers;

[Route("products")]
public class Products : ApiController
{
    private readonly ISender _mediator;

    public Products(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var createProductResult = await _mediator.Send(command);

        return createProductResult.Match(
            Product => Ok(createProductResult.Value),
            errors => Problem(errors)
        );
    }
}