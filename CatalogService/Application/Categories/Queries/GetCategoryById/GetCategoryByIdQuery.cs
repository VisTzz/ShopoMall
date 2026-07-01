using CatalogService.Application.Common.DTOs;
using MediatR;

namespace CatalogService.Application.Categories.Queries
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDTO>;
}
