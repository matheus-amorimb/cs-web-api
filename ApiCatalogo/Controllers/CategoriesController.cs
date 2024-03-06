using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<ICollection<Category>> GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            if (categories is null)
            {
                return NotFound();
            }
            return categories;
        }

        [HttpGet("{id:int}", Name = nameof(GetCategory))]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(item => item.CategoryId == id);
            if (category is null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        public ActionResult InsertCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            
            return new CreatedAtRouteResult(nameof(GetCategory), 
                new {id = category.CategoryId}, category); 
        }

        [HttpPut("{id:int}")]
        public ActionResult ModifyCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }
            _context.Categories.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteCategory(int id)
        {
            var categorie = _context.Categories.FirstOrDefault(item => item.CategoryId == id);

            if (categorie is null)
            {
                return BadRequest();
            }
            _context.Categories.Remove(categorie);
            _context.SaveChanges();

            return Ok(categorie);
        }
        
    }
}
