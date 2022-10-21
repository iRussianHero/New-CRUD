using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using New_CRUD.Models;

namespace DBCrud.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogDbContext blogDbContext;

        public PostController(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit(string id)
        {
            Post post = FindById(id);
            return View(post);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(blogDbContext.Posts.ToList());
        }

        public async Task<IActionResult> Delete(string id)
        {
            await DeleteById(id);
            return View("Index", blogDbContext.Posts.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Post post, IFormFile ImageUrl)
        {
            if (ImageUrl != null)
            {
                var filename = $"{Guid.NewGuid()}{Path.GetExtension(ImageUrl.FileName)}";
                using var fs = new FileStream(@$"wwwroot/uploads/{filename}", FileMode.Create);
                await ImageUrl.CopyToAsync(fs);
                post.ImageUrl = @$"/uploads/{filename}";
            }

            post.Date = DateTime.Now;

            blogDbContext.Posts.AddAsync(post);
            await blogDbContext.SaveChangesAsync();
            TempData["Status"] = "New post added!";
            return RedirectToAction("Index");

            //return View(post);
        }

        public Post FindById(string id)
        {
            Post post = new Post();
            return post = blogDbContext.Posts
                .Where(o => o.Id.ToString() == id)
                .FirstOrDefault();
        }
        public async Task<IActionResult> DeleteById(string id)
        {
            Post post = FindById(id);

            blogDbContext.Posts.Remove(post);
            await blogDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
