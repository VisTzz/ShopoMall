using CatalogService.Application.Common.DTOs;
using MediatR;

namespace CatalogService.Application.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery : IRequest<List<CategoryDTO>>;
}
