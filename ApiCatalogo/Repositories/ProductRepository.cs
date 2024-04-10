using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;
//================================================
//GENERIC REPOSITORY
//================================================
public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Product> GetProductsByCategory(int id)
    {
        return this.GetAll().Where(p => p.CategoryId == id);
    }
}

//================================================
//SPECIFIC REPOSITORY
//================================================
// public class ProductRepository : IProductRepository
// {
//
//     private readonly AppDbContext _context;
//
//     public ProductRepository(AppDbContext context)
//     {
//         _context = context;
//     }
//     
//     public IQueryable<Product> GetProducts()
//     {
//         return _context.Products; //retorna uma consulta que pode ser especificada
//     }
//
//     public Product GetProduct(int id)
//     {
//         return _context.Products.Find(id);
//     }
//
//     public Product Create(Product product)
//     {
//         _context.Products.Add(product);
//         _context.SaveChanges();
//
//         return product;
//     }
//
//     public bool Update(Product product)
//     {   
//         _context.Products.Update(product); //_context.Products.Entry(product).State = EntityState.Modified
//         _context.SaveChanges();
//         return true;
//     }
//
//     public bool Delete(int id)
//     {
//         var product = _context.Products.Find(id);
//         _context.Products.Remove(product);
//         _context.SaveChanges();
//
//         return true;
//     }
// }