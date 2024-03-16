using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repositoy;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public CategoriesController(ICategoryRepository repository, IConfiguration configuration, ILogger<CategoriesController> logger)
        {
            _repositoy = repository;
            _configuration = configuration;
            _logger = logger;
        }
        
        //##################################################################################
        //############################ USING REPOSITORY PATTERN ############################
        //##################################################################################

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            var categories = _repositoy.GetCategories().ToList();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _repositoy.GetCategory(id);

            if (category is null)
            {
                _logger.LogWarning($"Category with id = {id} not found...");
                return BadRequest($"Category with id = {id} not found...");
            }
            
            return Ok(category);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                _logger.LogWarning("Data invalid");
                return BadRequest("Data invalid");
            }

            _repositoy.Update(category);
            
            return Ok(category);
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var category = _repositoy.GetCategory(id);
            
            if (id != category.CategoryId)
            {
                _logger.LogWarning("Data invalid");
                return BadRequest("Data invalid");
            }

            _repositoy.Update(category);
            
            return Ok(category);
        }
        
        
        
        
        
        
        
        
        //###################################################################################
        //############################## USING DEFAULT PATTERN ##############################
        //###################################################################################
        // [HttpGet("ReadSettingsFile")]
        // public string GetConfigSettings()
        // {
        //     var key1 = _configuration["key1"];
        //     var key2 = _configuration["key2"];
        //     var sectionTestKey1 = _configuration["sectionTest:key1"];
        //     var sectionTestKey2 = _configuration["sectionTest:key2"];
        //     
        //     var data = new
        //     {
        //         key1 = key1,
        //         key2 = key2,
        //         sectionTestKey1 = sectionTestKey1,
        //         sectionTestKey2 = sectionTestKey2
        //     };
        //     
        //     string jsonResult = JsonConvert.SerializeObject(data);
        //
        //     return jsonResult;
        // }
        //
        // [HttpGet("UsingFromServices/{name}")]
        // public ActionResult<string> GetGreetingFromServices([FromServices] IMyService myService, string name)
        // {
        //     return myService.Greeting(name);
        // }
        //
        // [HttpGet("NotUsingFromServices/{name}")]
        // public ActionResult<string> GetGreetingNotFromServices(IMyService myService, string name)
        // {
        //     return myService.Greeting(name);
        // }   
        //
        // [HttpGet]
        // [ServiceFilter(typeof(ApiLoggingFilter))]
        // public ActionResult<ICollection<Category>> GetAllCategories()
        // {
        //     try
        //     {
        //         var categories = _repositoy.Categories.AsNoTracking().ToList();
        //         if (categories is null)
        //         {
        //             return NotFound();
        //         }
        //         return categories;
        //         // throw new DataMisalignedException();
        //     }
        //     catch (Exception e)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError,
        //             "Ocorreu um erro ao tratar a sua solicitação.");
        //     }
        // }
        //
        // [HttpGet("products")]
        // public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        // {
        //     _logger.LogInformation("=============== GET api/Categories/products ===============");
        //     return _repositoy.Categories.AsNoTracking().Include(item => item.Products).ToList();
        // }
        //
        // [HttpGet("{id:int:min(1)}", Name = nameof(GetCategory))]
        // public ActionResult<Category> GetCategory(int id)
        // {
        //     var category = _repositoy.Categories.AsNoTracking().FirstOrDefault(item => item.CategoryId == id);
        //     if (category is null)
        //     {
        //         return NotFound($"Categoria com id = {id} não encontrada...");
        //     }
        //     return category;
        // }
        //
        // [HttpPost]
        // public ActionResult InsertCategory(Category category)
        // {
        //     _repositoy.Categories.Add(category);
        //     _repositoy.SaveChanges();
        //     
        //     return new CreatedAtRouteResult(nameof(GetCategory), 
        //         new {id = category.CategoryId}, category); 
        // }
        //
        // [HttpPut("{id:int}")]
        // public ActionResult ModifyCategory(int id, Category category)
        // {
        //     if (id != category.CategoryId)
        //     {
        //         return BadRequest();
        //     }
        //     _repositoy.Categories.Entry(category).State = EntityState.Modified;
        //     _repositoy.SaveChanges();
        //
        //     return Ok(category);
        // }
        //
        // [HttpDelete("{id:int}")]
        // public ActionResult DeleteCategory(int id)
        // {
        //     var category = _repositoy.Categories.FirstOrDefault(item => item.CategoryId == id);
        //
        //     if (category is null)
        //     {
        //         return BadRequest();
        //     }
        //     _repositoy.Categories.Remove(category);
        //     _repositoy.SaveChanges();
        //
        //     return Ok(category);
        // }
        
    }
}
