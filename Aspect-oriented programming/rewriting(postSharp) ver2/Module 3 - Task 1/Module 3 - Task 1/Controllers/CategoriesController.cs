namespace Module_3___Task_1.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Module3;

    public class CategoriesController : Controller
    {
        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = this._context.Categories;
            return this.View(categories);
        }

        [HttpGet]
        public ActionResult GetCategory(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var categories = this._context.Categories.Find(id);

            if (categories == null)
            {
                return this.NotFound();
            }

            return this.View(categories);
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var category = this._context.Categories.Find(id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost, ActionName("EditCategory")]
        public ActionResult EditCategory(Categories category)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Entry(category).State = EntityState.Modified;
                this._context.SaveChanges();
                return this.RedirectToAction("GetCategories");
            }

            return this.View();
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return this.View();
        }

        [HttpPost, ActionName("CreateCategory")]
        public ActionResult CreateCategory(Categories category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this._context.Categories.Add(category);
            this._context.SaveChanges();
            return this.RedirectToAction("GetCategories");
        }

        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            var category = this._context.Categories.Find(id);
            if (category == null)
            {
                return this.NotFound();
            }

            try
            {
                this._context.Categories.Remove(category);
                this._context.SaveChanges();
            }
            catch (Exception e)
            {
                return this.RedirectToAction("GetCategories");
            }

            return this.RedirectToAction("GetCategories");
        }
    }
}