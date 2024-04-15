using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Parameters;
using ApiCatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // private readonly IProductRepository _unitOfWork.ProductRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            // _unitOfWork.ProductRepository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAll().ToList();
            if (products is null)
            {
                return NotFound();
            }
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<ProductDto>> Get([FromQuery] ProductsParameter productsParameter)
        {
            var products = _unitOfWork.ProductRepository.GetProducts(productsParameter);

            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productsDto);
        }
        
        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound();
            }
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(product);
            return Ok(productsDto);
        }

        [HttpGet("Category/{id:int}")]
        public ActionResult<IEnumerable<ProductDto>> GetProductsByCategory(int id)
        {
            var products = _unitOfWork.ProductRepository.GetProductsByCategory(id).ToList();
            
            if (products is null)
            {
                return NotFound(); 
            }
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(products);
            
        }
        
        [HttpPost]
        public ActionResult<Product> PostProduct(ProductDto productDto)
        {
            if (productDto is null)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDto);
            
            var newProduct = _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.Commit();

            var newProductDto = _mapper.Map<ProductDto>(newProduct);
            
            return new CreatedAtRouteResult("GetProduct",
                new { id = newProduct.ProductId }, newProductDto);
        }

        [HttpPatch("{id}/UpdatePartial")]
        public ActionResult<ProductDtoUpdateResponse> Patch(int id,
            JsonPatchDocument<ProductDtoUpdateRequest> patchProductDto)
        {

            if (patchProductDto is null || id <= 0)
                return BadRequest();

            var product = _unitOfWork.ProductRepository.GetById(c => c.ProductId == id);

            if (product is null)
            {
                return NotFound();
            }

            var productUpdateRequest = _mapper.Map<ProductDtoUpdateRequest>(product);
            
            patchProductDto.ApplyTo(productUpdateRequest, ModelState);

            if (!ModelState.IsValid || TryValidateModel(productUpdateRequest))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(productUpdateRequest, product);

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();

            return Ok(_mapper.Map<ProductDtoUpdateRequest>(product));
        }
        
        
        [HttpPut("{id:int}")]
        public ActionResult<Product> UpdateProduct(int id, ProductDto productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDto);
            
            var isUpdated = _unitOfWork.ProductRepository.Update(product);

            _unitOfWork.Commit();
            
            return isUpdated is not null ? Ok(productDto) : StatusCode(500, $"Fail to update the product of id = {id}");

        }

        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(p => p.ProductId == id);
            var isDeleted = _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Commit();

            var productDto = _mapper.Map<ProductDto>(product);
            
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