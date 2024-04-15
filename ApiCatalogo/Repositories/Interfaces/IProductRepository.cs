using ApiCatalogo.Models;
using ApiCatalogo.Parameters;

namespace ApiCatalogo.Repositories;

//================================================
//GENERIC REPOSITORY
//================================================
public interface IProductRepository : IRepository<Product>
{
    // IEnumerable<Product> GetProducts(ProductsParameter productsParameter);
    PagedList<Product> GetProducts(ProductsParameter productsParameter);
    IEnumerable<Product> GetProductsByCategory(int id);
}

//================================================
//SPECIFIC REPOSITORY
//================================================
// public interface IProductRepository
// {
//     IQueryable<Product> GetProducts();
//     Product GetProduct(int id);
//     Product Create(Product product);
//     bool Update(Product product);
//     bool Delete(int id);
// }