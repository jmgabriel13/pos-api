using Application.Categories.Create;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Pos_api.Abstraction;

namespace Pos_api.Controllers;

[Route("api/category")]
public class CategoryController : ApiController
{
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest createCategoryCommand, CancellationToken cancellationToken)
    {
        var command = new CreateCategoryCommand(createCategoryCommand.Name);

        Result result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
