using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperLogistics.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SuperLogistics.Controllers
{
    public class ProductsController : Controller

    {
        private readonly ProductsContext db;

        public ProductsController(ProductsContext context)
        {
            db = context;
        }

        // GET: ProductsController

        [Authorize]
        
        public ActionResult Index()
        {
            ViewBag.sessioncreate = HttpContext.Session.GetString("Added New Product");
            ViewBag.sessionedit = HttpContext.Session.GetString("Edited");
            return View("Index", db.Products.OrderBy(c => c.Name));
        }

        // GET: ProductsController/Details/5
      [Authorize]
      [AllowAnonymous]
       public IActionResult Details(string id)
        {
            return View("Details", db.Products.Find(id));
       }
        [Authorize]
        // GET: ProductsController/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                db.Add(newProduct); 
                db.SaveChanges(); 

             
                HttpContext.Session.SetString("Added New Product", "Added Sucessfully");
                return RedirectToAction("Index");
            }
            return View();

        }
        [Authorize]
        // GET: ProductsController/Edit/5
        public ActionResult Edit(string id)
        {
            return View("Edit", db.Products.Find(id)) ;
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("EAN,Name,Stock,Value,StoragePlacement")] Product product)
        {
       

            if (ModelState.IsValid)
            
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                HttpContext.Session.SetString("Edited", "Edited Sucessfully");
                return RedirectToAction("Index");
                }
                return View(product);

        }
        [Authorize]
        // GET: ProductsController/Delete/5
        public ActionResult Delete(string id)
        {
            return View("Delete", db.Products.Find(id));
        }


        // POST: ProductsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }
        [Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> Search(String ProductSearch)
        {
            ViewData["GetProductDetails"] = ProductSearch;
            var productquerry = from x in db.Products select x;
            if (!String.IsNullOrEmpty(ProductSearch))
            {
                productquerry = productquerry.Where(x => x.EAN.Contains(ProductSearch) || x.Name.Contains(ProductSearch));
            }
            return View(await productquerry.AsNoTracking().ToListAsync());
        }

    }
}
