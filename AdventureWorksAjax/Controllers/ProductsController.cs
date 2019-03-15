using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEFCodeFirst.Models;
using X.PagedList;

namespace MVCEFCodeFirst.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AdventureWorks db;

        public ProductsController()
        {
            db = new AdventureWorks();
        }

        // Now here's a constructor for this class that takes an AdventureWorks context as an arg
        // if we re-write the call to instantiate this products to include an arg, we could get rid of
        // the default constructor (above), and get rid of the "new"
        // Then we'd have Dependency Injection!
        public ProductsController(AdventureWorks useThisDatabaseInstead)
        {
            db = useThisDatabaseInstead;
        }

        // This was the original line:
        //private AdventureWorks db = new AdventureWorks();

        // GET: Products
        //public ActionResult Index()
        //{
        //    var products = db.Products.Include(p => p.ProductCategory).Include(p => p.ProductModel);
        //    return View(products.ToList());
        //}

        public ActionResult Index(int? page)
        {
            var products = db.Products
               //.Include(p => p.ProductCategory)
               //.Include(p => p.ProductModel)
               .Where(p => p.ThumbnailPhotoFileName != "no_image_available_small.gif")
               .OrderBy(p => p.ProductCategory.Name)
               .ThenBy(p => p.ProductModel.Name)
               .ThenBy(p => p.Name);

            // or you could use the Query syntax:
            //var products =
            //(from p in db.Products where p.ThumbnailPhotoFileName != "no_image_available_small.gif" select p).OrderBy(p => p.ProductCategory.Name);

            // if no page was specified in the querystring, default to 
            // the first page (1):

            int pageNumber = page ?? 1;
            int pageSize = 5;

            if (Request.IsAjaxRequest())
            {
                return (ActionResult)PartialView("_ProductList", products.ToPagedList(pageNumber, pageSize));
            }
            return View(products.ToPagedList(pageNumber, pageSize));
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name");
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,rowguid,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", product.ProductCategoryID);
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name", product.ProductModelID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", product.ProductCategoryID);
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name", product.ProductModelID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbnailPhotoFileName,rowguid,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", product.ProductCategoryID);
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name", product.ProductModelID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
