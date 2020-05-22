namespace Module_3___Task_1.Controllers
{
    using System;
    using System.Linq;

    using Castle.DynamicProxy;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using Module3;

    using WebShop.LoggingCastelCoreLib;

    public class ProductsController : Controller
    {
        /// <summary>
        /// The helper.
        /// </summary>
        private readonly IBaseHelper helper;

        /// <summary>
        /// The _context.
        /// </summary>
        private readonly NorthwindContext _context;

        /// <summary>
        /// The _logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public ProductsController(NorthwindContext context, ILogger<ProductsController> logger)
        {
            this._context = context;
            this._logger = logger;

            var generator = new ProxyGenerator();
            this.helper = generator.CreateInterfaceProxyWithTarget<IBaseHelper>(new BaseEntityHelper(), new LogMethodCastelCore());
        }

        /// <summary>
        /// The get products.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult GetProducts()
        {
            var products = this._context.Products.Include(p => p.Category).Include(p => p.Supplier).ToList();
            this._logger.LogInformation("Products are find");
            return this.View(products);
        }

        [HttpPost]
        public ActionResult GetProducts(int count)
        {
            var products = this.helper.GetCertainAmountOfProduct(this._context, count);
            return this.View(products);
        }

        /// <summary>
        /// The get product.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult GetProduct(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var product = this._context.Products.Find(id);

            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        /// <summary>
        /// The edit product.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var product = this._context.Products.Find(id);

            if (product == null)
            {
                return this.NotFound();
            }

            SelectList categoryId = new SelectList(this._context.Categories, "CategoryId", "CategoryName");
            ViewBag.CategoryId = categoryId;

            SelectList supplierId = new SelectList(this._context.Suppliers, "SupplierId", "CompanyName");
            ViewBag.SupplierId = supplierId;

            return this.View(product);
        }

        /// <summary>
        /// The edit product.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost, ActionName("EditProduct")]
        public ActionResult EditProduct(Products product)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Entry(product).State = EntityState.Modified;
                this._context.SaveChanges();
                return this.RedirectToAction("GetProducts");
            }

            SelectList categoryId = new SelectList(this._context.Categories, "CategoryId", "CategoryName");
            this.ViewBag.CategoryId = categoryId;

            SelectList supplierId = new SelectList(this._context.Suppliers, "SupplierId", "CompanyName");
            this.ViewBag.SupplierId = supplierId;

            return this.View();
        }

        /// <summary>
        /// The create product.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult CreateProduct()
        {
            SelectList categoryId = new SelectList(this._context.Categories, "CategoryId", "CategoryName");
            ViewBag.CategoryId = categoryId;

            SelectList supplierId = new SelectList(this._context.Suppliers, "SupplierId", "CompanyName");
            this.ViewBag.SupplierId = supplierId;
            return this.View();
        }

        /// <summary>
        /// The create product.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost, ActionName("CreateProduct")]
        public ActionResult CreateProduct(Products product)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Products.Add(product);
                this._context.SaveChanges();
                return this.RedirectToAction("GetProducts");
            }

            SelectList categoryId = new SelectList(this._context.Categories, "CategoryId", "CategoryName");
            this.ViewBag.CategoryId = categoryId;

            SelectList supplierId = new SelectList(this._context.Suppliers, "SupplierId", "CompanyName");
            this.ViewBag.SupplierId = supplierId;

            return this.View();
        }

        /// <summary>
        /// The delete product.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            var product = this._context.Products.Find(id);
            if (product == null)
            {
                return this.NotFound();
            }

            try
            {
                this._context.Products.Remove(product);
                this._context.SaveChanges();
            }
            catch(Exception e)
            {
                this._logger.LogError($"Can't to delete product{product.ProductName}");
                return this.RedirectToAction("GetProducts");
            }

            return this.RedirectToAction("GetProducts");
        }
    }
}