using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _context.Products.ToList();
            if(products is null)
            {
                return NotFound();
            }

            return products;
        }      
        
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(item => item.ProductId == id);
            if(product is null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public ActionResult InsertProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges(); //Persiste os dados na tabela
            
            return new CreatedAtRouteResult(nameof(GetProduct), 
                new {id = product.ProductId}, product);
        }
        
        [HttpPut("{id:int}")]  
        public ActionResult ModifyProduct(int id, Product product)  
        {  
            if(id != product.ProductId)
            {
                return BadRequest();
            }
	
            _context.Entry(product).State = EntityState.Modified;  
            _context.SaveChanges(); 
    
            return Ok(product);
        }        
        
        [HttpDelete("{id:int}")]  
        public ActionResult DeleteProduct(int id)  
        {  
            
            var product = _context.Products.FirstOrDefault(item => item.ProductId == id);

            if (product is null)
            {
                return NotFound("Product not located");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            
            return Ok(product);
        }
        
        
    }
}