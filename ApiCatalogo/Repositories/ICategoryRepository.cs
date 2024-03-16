using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories;

public interface ICategorieRepository
{
    IEnumerable<Category> GetCategories();
    Category GetCategory(int id);
    Category Create(Category category);
    Category Update(Category category);
    Category Delete(int id);
}