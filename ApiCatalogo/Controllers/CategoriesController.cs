using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Parameters;
using ApiCatalogo.Repositories;
using ApiCatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using X.PagedList;

namespace ApiCatalogo.Controllers
{
//##################################################################################
//############################ USING GENERIC REPOSITORY ############################
//##################################################################################
    
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // private readonly IRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IConfiguration configuration,
            ILogger<CategoriesController> logger, IMapper mapper)
        {
            // _repository = repository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
        }
        
         [HttpGet]
         [Authorize]
         public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
         {
             var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
             var categoriesDto = categories.ToList().ToCategoryDtOs();
             return Ok(categoriesDto);
         }
         
         [HttpGet("pagination")]
         public async Task<ActionResult<IEnumerable<CategoryDto>>> Get([FromQuery] CategoriesParameter categoriesParameter)
         {
             var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync(categoriesParameter);
             return GetCategories(categories);
         }

         [HttpGet("filter/name/pagination")]
         public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesFilterName([FromQuery] CategoriesFilterName categoriesFilterName)
         {
             var categories = await _unitOfWork.CategoryRepository.GetCategoriesFilterNameAsync(categoriesFilterName);
             return GetCategories(categories);
         }

         private ActionResult<IEnumerable<CategoryDto>> GetCategories(IPagedList<Category> categories)
         {
             var metadata = new
             {
                 categories.Count,
                 categories.PageSize,
                 categories.PageCount,
                 categories.TotalItemCount,
                 categories.HasNextPage,
                 categories.HasPreviousPage
             };

             Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            
             var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

             return Ok(categoriesDto);
         }
         
         [HttpGet("{id:int}")]
         public async Task<ActionResult<CategoryDto>> GetCategory(int id)
         {
             var category = await _unitOfWork.CategoryRepository.GetByIdAsync(p => p.CategoryId == id);

             if (category is null)
             {
                 _logger.LogWarning($"Category with id = {id} not found...");
                 return BadRequest($"Category with id = {id} not found...");
             }

             var categoryDto = category.ToCategoryDto();
             
             return Ok(categoryDto);
         }
         
         [HttpPost]
         public async Task<ActionResult<CategoryDto>> PostCategory(Category category)
         {
             _unitOfWork.CategoryRepository.Create(category);
             
             await _unitOfWork.CommitAsync();

             var categoryDto = category.ToCategoryDto();
             
             return Ok(categoryDto);
         }
     
         [HttpPut("{id:int}")]
         public async Task<ActionResult> Put(int id, Category category)
         {
             if (id != category.CategoryId)
             {
                 _logger.LogWarning("Data invalid");
                 return BadRequest("Data invalid");
             }

             _unitOfWork.CategoryRepository.Update(category);
             await _unitOfWork.CommitAsync();
             
             var categoryDto = category.ToCategoryDto();

             return Ok(categoryDto);
         }
         
         [HttpDelete("{id:int}")]
         public async Task<ActionResult> Delete(int id)
         {
             var category = await _unitOfWork.CategoryRepository.GetByIdAsync(p => p.CategoryId == id);

             _unitOfWork.CategoryRepository.Delete(category);
             
             await _unitOfWork.CommitAsync();
            
             var categoryDto = category.ToCategoryDto();
             
             return Ok(categoryDto);
         }
    }
}
//##################################################################################
//############################ USING SPECIFIC REPOSITORY ###########################
//##################################################################################

/*[Route("api/[controller]")]
[ApiController]
//     public class CategoriesController : ControllerBase
//     {
//         private readonly ICategoryRepository _repository;
//         private readonly IConfiguration _configuration;
//         private readonly ILogger _logger;
//         public CategoriesController(ICategoryRepository repository, IConfiguration configuration, ILogger<CategoriesController> logger)
//         {
//             _repositoy = repository;
//             _configuration = configuration;
//             _logger = logger;
//         }
//
//         //##################################################################################
//         //############################ USING REPOSITORY PATTERN ############################
//         //##################################################################################
//
//         [HttpGet]
//         public ActionResult<IEnumerable<Category>> GetAllCategories()
//         {
//             var categories = _repository.GetCategories().ToList();
//             return Ok(categories);
//         }
//
//         [HttpGet("{id:int}")]
//         public ActionResult<Category> GetCategory(int id)
//         {
//             var category = _repository.GetCategory(id);
//
//             if (category is null)
//             {
//                 _logger.LogWarning($"Category with id = {id} not found...");
//                 return BadRequest($"Category with id = {id} not found...");
//             }
//
//             return Ok(category);
//         }
//
//         [HttpPost]
//         public ActionResult<Category> PostCategory(Category category)
//         {
//             _repository.Create(category);
//
//             return Ok(category);
//         }
//
//
//         [HttpPut("{id:int}")]
//         public ActionResult Put(int id, Category category)
//         {
//             // if (id != category.CategoryId)
//             // {
//             //     _logger.LogWarning("Data invalid");
//             //     return BadRequest("Data invalid");
//             // }
//
//             _repository.Update(category);
//
//             return Ok(category);
//         }
//
//         [HttpDelete("{id:int}")]
//         public ActionResult Delete(int id)
//         {
//             var category = _repository.GetCategory(id);
//
//             if (id != category.CategoryId)
//             {
//                 _logger.LogWarning("Data invalid");
//                 return BadRequest("Data invalid");
//             }
//
//             _repository.Delete(id);
//
//             return Ok(category);
//         }
//
//         //###################################################################################
//         //############################## USING DEFAULT PATTERN ##############################
//         //###################################################################################
//         // [HttpGet("ReadSettingsFile")]
//         // public string GetConfigSettings()
//         // {
//         //     var key1 = _configuration["key1"];
//         //     var key2 = _configuration["key2"];
//         //     var sectionTestKey1 = _configuration["sectionTest:key1"];
//         //     var sectionTestKey2 = _configuration["sectionTest:key2"];
//         //
//         //     var data = new
//         //     {
//         //         key1 = key1,
//         //         key2 = key2,
//         //         sectionTestKey1 = sectionTestKey1,
//         //         sectionTestKey2 = sectionTestKey2
//         //     };
//         //
//         //     string jsonResult = JsonConvert.SerializeObject(data);
//         //
//         //     return jsonResult;
//         // }
//         //
//         // [HttpGet("UsingFromServices/{name}")]
//         // public ActionResult<string> GetGreetingFromServices([FromServices] IMyService myService, string name)
//         // {
//         //     return myService.Greeting(name);
//         // }
//         //
//         // [HttpGet("NotUsingFromServices/{name}")]
//         // public ActionResult<string> GetGreetingNotFromServices(IMyService myService, string name)
//         // {
//         //     return myService.Greeting(name);
//         // }
//         //
//         // [HttpGet]
//         // [ServiceFilter(typeof(ApiLoggingFilter))]
//         // public ActionResult<ICollection<Category>> GetAllCategories()
//         // {
//         //     try
//         //     {
//         //         var categories = _repositoy.Categories.AsNoTracking().ToList();
//         //         if (categories is null)
//         //         {
//         //             return NotFound();
//         //         }
//         //         return categories;
//         //         // throw new DataMisalignedException();
//         //     }
//         //     catch (Exception e)
//         //     {
//         //         return StatusCode(StatusCodes.Status500InternalServerError,
//         //             "Ocorreu um erro ao tratar a sua solicitação.");
//         //     }
//         // }
//         //
//         // [HttpGet("products")]
//         // public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
//         // {
//         //     _logger.LogInformation("=============== GET api/Categories/products ===============");
//         //     return _repositoy.Categories.AsNoTracking().Include(item => item.Products).ToList();
//         // }
//         //
//         // [HttpGet("{id:int:min(1)}", Name = nameof(GetCategory))]
//         // public ActionResult<Category> GetCategory(int id)
//         // {
//         //     var category = _repositoy.Categories.AsNoTracking().FirstOrDefault(item => item.CategoryId == id);
//         //     if (category is null)
//         //     {
//         //         return NotFound($"Categoria com id = {id} não encontrada...");
//         //     }
//         //     return category;
//         // }
//         //
//         // [HttpPost]
//         // public ActionResult InsertCategory(Category category)
//         // {
//         //     _repositoy.Categories.Add(category);
//         //     _repositoy.SaveChanges();
//         //
//         //     return new CreatedAtRouteResult(nameof(GetCategory),
//         //         new {id = category.CategoryId}, category);
//         // }
//         //
//         // [HttpPut("{id:int}")]
//         // public ActionResult ModifyCategory(int id, Category category)
//         // {
//         //     if (id != category.CategoryId)
//         //     {
//         //         return BadRequest();
//         //     }
//         //     _repositoy.Categories.Entry(category).State = EntityState.Modified;
//         //     _repositoy.SaveChanges();
//         //
//         //     return Ok(category);
//         // }
//         //
//         // [HttpDelete("{id:int}")]
//         // public ActionResult DeleteCategory(int id)
//         // {
//         //     var category = _repositoy.Categories.FirstOrDefault(item => item.CategoryId == id);
//         //
//         //     if (category is null)
//         //     {
//         //         return BadRequest();
//         //     }
//         //     _repositoy.Categories.Remove(category);
//         //     _repositoy.SaveChanges();
//         //
//         //     return Ok(category);
//         // }
//
//     }
// }*/
