using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateCategoryRequest request, CancellationToken ct)
        {
            var command = new CreateCategoryCommand(request.Name, request.ParentId);
            var categoryId = await _mediator.Send(command, ct);

            return CreatedAtAction(nameof(GetById), new { id = categoryId }, categoryId);
        }

        [HttpGet("{id:guid}")]

        public async Task<ActionResult<CategoryDto>> GetById(Guid Id, CancellationToken ct)
        {
            try
            {
                var query = new GetCategoryByIdQuery(Id);
                var result = await _mediator.Send(query, ct);

                return Ok(result);
            } catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

        public record CreateCategoryRequest(string Name, Guid? ParentId);
        public record CategoryDto(Guid Id, string Name);
    }
}
