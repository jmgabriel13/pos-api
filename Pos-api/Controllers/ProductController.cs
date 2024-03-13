using Application.Products.Create;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Pos_api.Abstraction;

namespace Pos_api.Controllers;

[Route("api/products")]
public class ProductController : ApiController
{
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest createProductRequest, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
            createProductRequest.Name,
            createProductRequest.Description,
            createProductRequest.Price,
            createProductRequest.Sku,
            createProductRequest.Stocks,
            createProductRequest.Categories,
            createProductRequest.Sizes,
            createProductRequest.SideDishes);

        Result result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
