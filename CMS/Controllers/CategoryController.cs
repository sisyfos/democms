using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers
{
    public class CategoryController : Controller
    {
        private cms_2Entities db = new cms_2Entities();

        //
        // GET: /Category/

        public ActionResult Index()
        {
            var categories = db.Categories.Include("Template");
            return View(categories.ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(long id = 0)
        {
            Category category = db.Categories.Single(c => c.CatID == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            ViewBag.TempID = new SelectList(db.Templates, "TempID", "TempName");
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.AddObject(category);
                db.SaveChanges();
                return new RedirectResult("~/Admin/Content/" + category.CatID, false);
            }

            ViewBag.TempID = new SelectList(db.Templates, "TempID", "TempName", category.TempID);
            return View(category);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Category category = db.Categories.Single(c => c.CatID == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.TempID = new SelectList(db.Templates, "TempID", "TempName", category.TempID);
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(category);
                db.ObjectStateManager.ChangeObjectState(category, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TempID = new SelectList(db.Templates, "TempID", "TempName", category.TempID);
            return View(category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Category category = db.Categories.Single(c => c.CatID == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Category category = db.Categories.Single(c => c.CatID == id);
            db.Categories.DeleteObject(category);
            db.SaveChanges();
            return RedirectToAction("Page", "Admin");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}