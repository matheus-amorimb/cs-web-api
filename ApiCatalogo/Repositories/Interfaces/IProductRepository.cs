using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories;

//================================================
//GENERIC REPOSITORY
//================================================
public interface IProductRepository : IRepository<Product>
{
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