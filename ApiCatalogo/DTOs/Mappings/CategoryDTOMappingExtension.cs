using ApiCatalogo.Models;
using NuGet.Common;

namespace ApiCatalogo.DTOs.Mappings;

public static class CategoryDTOMappingExtension
{
    public static CategoryDTO? ToCategoryDto(this Category category)
    {
        if (category is null) return null;

        return new CategoryDTO()
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            UrlImage = category.UrlImage
        };
    }    
    
    public static Category? ToCategory(this CategoryDTO vCategoryDto)
    {
        if (vCategoryDto is null) return null;

        return new Category()
        {
            CategoryId = vCategoryDto.CategoryId,
            Name = vCategoryDto.Name,
            UrlImage = vCategoryDto.UrlImage
        };
    }
    
    public static IEnumerable<CategoryDTO?> ToCategoryDtOs(this IEnumerable<Category> categories)
    {
        if (categories is null) return new List<CategoryDTO>();
        
        return categories.Select(ToCategoryDto);
        
    }
}