using ApiCatalogo.Models;
using ApiCatalogo.Parameters;

namespace ApiCatalogo.Repositories;

//================================================
//GENERIC REPOSITORY
//================================================
public interface ICategoryRepository : IRepository<Category>
{
    PagedList<Category> GetCategories(CategoriesParameter categoriesParameter);
    //
}


//================================================
//SPECIFIC REPOSITORY
//================================================
// public interface ICategoryRepository<T> : IRepository<T>
// {
//     IEnumerable<Category> GetCategories();
//     Category GetCategory(int id);
//     Category Create(Category category);
//     Category Update(Category category);
//     Category Delete(int id);
// }