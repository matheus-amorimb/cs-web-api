using ApiCatalogo.Models;
using ApiCatalogo.Parameters;
using X.PagedList;

namespace ApiCatalogo.Repositories;

//================================================
//GENERIC REPOSITORY
//================================================
public interface ICategoryRepository : IRepository<Category>
{
    Task<IPagedList<Category>> GetCategoriesFilterNameAsync(CategoriesFilterName categoriesFilterName);
    Task<IPagedList<Category>> GetCategoriesAsync(CategoriesParameter categoriesParameter);
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