using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Categories.Queries;
using CatalogService.Application.Categories.Queries.GetAllCategories;
using CatalogService.Application.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
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
        public async Task<ActionResult<CategoryDTO>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var query = new GetCategoryByIdQuery(id);
                var result = await _mediator.Send(query, ct);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll(CancellationToken ct)
        {
            var query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }
    }

    public record CreateCategoryRequest(string Name, Guid? ParentId);
}
