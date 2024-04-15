using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Parameters;
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

    // public IEnumerable<Product> GetProducts(ProductsParameter productsParameter)
    // {   
    //     return this.GetAll()
    //         .OrderBy(p => p.Name)
    //         .Skip((productsParameter.PageNumber - 1) * productsParameter.PageSize)
    //         .Take(productsParameter.PageSize).ToList();
    // }

    public PagedList<Product> GetProducts(ProductsParameter productsParameter)
    {
        var products = this
            .GetAll()
            .OrderBy(p => p.ProductId)
            .AsQueryable();

        var productsSorted = PagedList<Product>.ToPagedList(products,
            productsParameter.PageNumber, 
            productsParameter.PageSize);

        return productsSorted;
    }

    public PagedList<Product> GetProductsFilterPrice(ProductsFilterPrice productsFilterPrice)
    {
        var products = this.GetAll().AsQueryable();

        if (productsFilterPrice.Price.HasValue && !string.IsNullOrEmpty(productsFilterPrice.PriceCriteria))
        {
            if (productsFilterPrice.PriceCriteria.Equals("greater", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(p => p.Price > productsFilterPrice.Price.Value)
                    .OrderBy(p => p.Price);
            }
            else if (productsFilterPrice.PriceCriteria.Equals("lower", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(p => p.Price < productsFilterPrice.Price.Value)
                    .OrderBy(p => p.Price);
            }
            else if (productsFilterPrice.PriceCriteria.Equals("equal", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(p => p.Price == productsFilterPrice.Price.Value)
                    .OrderBy(p => p.Price);
            }
        }
        
        var filteredProducts = PagedList<Product>.ToPagedList(products, productsFilterPrice.PageNumber, productsFilterPrice.PageSize);
        
        return filteredProducts;
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