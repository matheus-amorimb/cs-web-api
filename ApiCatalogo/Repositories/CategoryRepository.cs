using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

//================================================
//GENERIC REPOSITORY
//================================================
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public PagedList<Category> GetCategories(CategoriesParameter categoriesParameter)
    {
        var categories = this
            .GetAll()
            .OrderBy(c => c.CategoryId)
            .AsQueryable();

        var categoriesSorted = PagedList<Category>.ToPagedList(categories,
            categoriesParameter.PageNumber, 
            categoriesParameter.PageSize);

        return categoriesSorted;
    }
}

//================================================
//SPECIFIC REPOSITORY
//================================================
// public class CategoryRepository : ICategoryRepository
// {
//     ================================================
//     GENERIC REPOSITORY
//     ================================================
//     
//     
//     ================================================
//     SPECIFIC REPOSITORY
//     ================================================
//      private readonly AppDbContext _context;
//     
//      public CategoryRepository(AppDbContext context)
//      {
//          _context = context;
//      }
//     
//      public IEnumerable<Category> GetCategories()
//      {
//          return _context.Categories.ToList();
//      }
//     
//      public Category GetCategory(int id)
//      {
//          return _context.Categories.Find(id);
//      }
//     
//      public Category Create(Category category)
//      {
//          _context.Categories.Add(category); //incluir na memória
//          _context.SaveChanges();
//     
//          return category;
//      }
//     
//      public Category Update(Category category)
//      {
//          _context.Categories.Entry(category).State = EntityState.Modified; //já está na memória, é preciso modificar o estado
//          _context.SaveChanges();
//     
//          return category;
//      }
//     
//      public Category Delete(int id)
//      {
//          var category = _context.Categories.Find(id);
//          _context.Categories.Remove(category);
//          _context.SaveChanges();
//     
//          return category;
//      }
// }