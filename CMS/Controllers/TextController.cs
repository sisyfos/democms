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
    public class TextController : Controller
    {
        private cms_2Entities db = new cms_2Entities();

        //
        // GET: /Text/

        public ActionResult Index()
        {
            var texts = db.Texts.Include("Category");
            return View(texts.ToList());
        }

        //
        // GET: /Text/Details/5

        public ActionResult Details(long id = 0)
        {
            Text text = db.Texts.Single(t => t.TextID == id);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }

        //
        // GET: /Text/Create

        public ActionResult Create(int? id)
        {

            ViewBag.CatID = id;
            var category = db.Categories.First(c => c.CatID == id.Value);
            ViewBag.CatName = category.CatName;
                
            return View();
        }

        //
        // POST: /Text/Create

        [HttpPost]
        public ActionResult Create(Text text)
        {
            if (ModelState.IsValid)
            {
                db.Texts.AddObject(text);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", text.CatID);
            return View(text);
        }

        //
        // GET: /Text/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Text text = db.Texts.Single(t => t.TextID == id);
            if (text == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", text.CatID);
            return View(text);
        }

        //
        // POST: /Text/Edit/5

        [HttpPost]
        public ActionResult Edit(Text text)
        {
            if (ModelState.IsValid)
            {
                db.Texts.Attach(text);
                db.ObjectStateManager.ChangeObjectState(text, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", text.CatID);
            return View(text);
        }

        //
        // GET: /Text/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Text text = db.Texts.Single(t => t.TextID == id);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }

        //
        // POST: /Text/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Text text = db.Texts.Single(t => t.TextID == id);
            db.Texts.DeleteObject(text);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}