using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;

namespace MyAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        MySaleDBContext context = new MySaleDBContext();

        [HttpGet]
        public IActionResult Get()
        {
            var data = context.Categories.ToList();
            return Ok(data);
        }

        //Tao api/get/id: Get Category by id
        //Neus khong get dc thi msg "Category not exist"
        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            var data = context.Categories.Where(x=>x.CategoryId == id).FirstOrDefault();
            if (data == null)
            {
                return BadRequest("Category not exist");
            }
            return Ok(data);

        }

        //Tao api/post/category: insert category to db
        //CHeck name empty
        [HttpPost]
        public IActionResult Post(Category category)
        {
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                return BadRequest("Name is not empty");
            }
            context.Add(category);
            context.SaveChanges();
            return Ok(category);
        }

        //Tao api put/category: update category to db
        //Check id co ton tai khong
        [HttpPut]
        public IActionResult Put(Category category)
        {
            var cate = context.Categories.Where(x=>x.CategoryId==category.CategoryId).FirstOrDefault();
            if(cate == null)
            {
                return BadRequest("category not found");
            }
            cate.CategoryName = category.CategoryName;
            context.Categories.Update(cate);
            context.SaveChanges();
            return Ok(cate);
        }

        //Tao api delete/id: delete category by id
        //Check xem id cos ton tai khong
        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var cate = context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            var products = context.Products.Where(x => x.CategoryId == id).ToList();
            if (cate == null)
            {
                return BadRequest("category not found");
            }
            foreach(Product p in products)
            {
                context.Remove(p);
                context.SaveChanges();
            }
            context.Categories.Remove(cate);
            context.SaveChanges();
            return Ok(cate);
        }
    }


}
