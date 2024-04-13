using ApiCatalogo.Models;
using NuGet.Common;

namespace ApiCatalogo.DTOs.Mappings;

public static class CategoryDTOMappingExtension
{
    public static CategoryDto? ToCategoryDto(this Category category)
    {
        if (category is null) return null;

        return new CategoryDto()
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            UrlImage = category.UrlImage
        };
    }    
    
    public static Category? ToCategory(this CategoryDto vCategoryDto)
    {
        if (vCategoryDto is null) return null;

        return new Category()
        {
            CategoryId = vCategoryDto.CategoryId,
            Name = vCategoryDto.Name,
            UrlImage = vCategoryDto.UrlImage
        };
    }
    
    public static IEnumerable<CategoryDto?> ToCategoryDtOs(this IEnumerable<Category> categories)
    {
        if (categories is null) return new List<CategoryDto>();
        
        return categories.Select(ToCategoryDto);
        
    }
}