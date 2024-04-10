using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // private readonly IProductRepository _unitOfWork.ProductRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            // _unitOfWork.ProductRepository = repository;
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAll().ToList();
            if (products is null)
            {
                return NotFound();
            }

            return Ok(products);
        }
        
        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("Category/{id:int}")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategory(int id)
        {
            var products = _unitOfWork.ProductRepository.GetProductsByCategory(id).ToList();
            
            if (products is null)
            {
                return NotFound(); 
            }

            return Ok(products);
            
        }
        
        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            if (product is null)
            {
                return BadRequest();
            }

            var newProduct = _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("GetProduct",
                new { id = newProduct.ProductId }, newProduct);
        }
        
        [HttpPut("{id:int}")]
        public ActionResult<Product> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var isUpdated = _unitOfWork.ProductRepository.Update(product);

            _unitOfWork.Commit();
            
            return isUpdated is not null ? Ok(product) : StatusCode(500, $"Fail to update the product of id = {id}");

        }

        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(p => p.ProductId == id);
            var isDeleted = _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Commit();
            
            return isDeleted is not null ? Ok($"Product of id = {id} is deleted") : StatusCode(500, $"Fail to delete the product of id = {id}");
        }
    }
    
}
/*
[Route("[controller]")]
[ApiController]
public class ProductsController(IProductRepository repository) : ControllerBase
{
    private readonly IProductRepository _unitOfWork.ProductRepository = repository;
    
//##################################################################################
//############################ USING REPOSITORY PATTERN ############################
//##################################################################################

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAllProducts()
    {
        var products = _unitOfWork.ProductRepository.GetProducts().ToList();
        if (products is null)
        {
            return NotFound();
        }

        return Ok(products);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _unitOfWork.ProductRepository.GetProduct(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> PostProduct(Product product)
    {
        if (product is null)
        {
            return BadRequest();
        }

        var newProduct = _unitOfWork.ProductRepository.Create(product);

        return new CreatedAtRouteResult("GetProduct",
            new { id = newProduct.ProductId }, newProduct);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Product> UpdateProduct(int id, Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest();
        }

        var isUpdated = _unitOfWork.ProductRepository.Update(product);

        return isUpdated ? Ok(product) : StatusCode(500, $"Fail to update the product of id = {id}");

    }

    [HttpDelete("{id:int}")]
    public ActionResult<bool> DeleteProduct(int id)
    {
        var isDeleted = _unitOfWork.ProductRepository.Delete(id);
        return isDeleted ? Ok($"Product of id = {id} is deleted") : StatusCode(500, $"Fail to delete the product of id = {id}");
    }
}

//###################################################################################
//############################## USING DEFAULT PATTERN ##############################
//###################################################################################
//
//     [HttpGet]
//     public ActionResult<IEnumerable<Product>> GetAllProducts()
//     {
//         var products = _context.Products.AsNoTracking().ToList();
//         if(products is null)
//         {
//             return NotFound();
//         }
//
//         return products;
//     }
//
//     [HttpGet("{valor:alpha:length(5)}")]
//     public ActionResult<Product> GetFirstProduct()
//     {
//         return _context.Products.FirstOrDefault();
//     }
//
//     [HttpGet("{id:int}")]
//     public async Task<ActionResult<Product>> GetProduct([FromQuery] int id)
//     {
//
//         var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(item => item.ProductId == id);
//         if(product is null)
//         {
//             return NotFound();
//         }
//
//         return product;
//     }
//
//     [HttpPost]
//     public ActionResult InsertProduct(Product product)
//     {
//         _context.Products.Add(product);
//         _context.SaveChanges(); //Persiste os dados na tabela
//
//         return new CreatedAtRouteResult(nameof(GetProduct),
//             new {id = product.ProductId}, product);
//     }
//
//     [HttpPut("{id:int}")]
//     public ActionResult ModifyProduct(int id, Product product)
//     {
//         if(id != product.ProductId)
//         {
//             return BadRequest();
//         }
//
//         _context.Entry(product).State = EntityState.Modified;
//         _context.SaveChanges();
//
//         return Ok(product);
//     }
//
//     [HttpDelete("{id:int}")]
//     public ActionResult DeleteProduct(int id)
//     {
//
//         var product = _context.Products.FirstOrDefault(item => item.ProductId == id);
//
//         if (product is null)
//         {
//             return NotFound("Product not located");
//         }
//
//         _context.Products.Remove(product);
//         _context.SaveChanges();
//
//         return Ok(product);
//     }
//
//}


}
*/