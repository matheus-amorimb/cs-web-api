using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories;

//================================================
//GENERIC REPOSITORY
//================================================
public interface ICategoryRepository : IRepository<Category>
{
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