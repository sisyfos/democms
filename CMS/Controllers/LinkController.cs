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
    public class LinkController : Controller
    {
        private cms_2Entities db = new cms_2Entities();

        //
        // GET: /Link/

        public ActionResult Index()
        {
            var links = db.Links.Include("Category");
            return View(links.ToList());
        }

        //
        // GET: /Link/Details/5

        public ActionResult Details(long id = 0)
        {
            Link link = db.Links.Single(l => l.LinkID == id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        //
        // GET: /Link/Create

        public ActionResult Create(int? id)
        {

            ViewBag.CatID = id;
            var category = db.Categories.First(c => c.CatID == id.Value);
            ViewBag.CatName = category.CatName;

            return View();
        }

        //
        // POST: /Link/Create

        [HttpPost]
        public ActionResult Create(Link link)
        {
            if (ModelState.IsValid)
            {
                db.Links.AddObject(link);
                db.SaveChanges();
                return RedirectToAction("Content", "Admin", new { id = link.CatID });
            }

            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", link.CatID);
            return View(link);
        }

        //
        // GET: /Link/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Link link = db.Links.Single(l => l.LinkID == id);
            if (link == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", link.CatID);
            return View(link);
        }

        //
        // POST: /Link/Edit/5

        [HttpPost]
        public ActionResult Edit(Link link)
        {
            if (ModelState.IsValid)
            {
                db.Links.Attach(link);
                db.ObjectStateManager.ChangeObjectState(link, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Content", "Admin", new { id = link.CatID });
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", link.CatID);
            return View(link);
        }

        //
        // GET: /Link/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Link link = db.Links.Single(l => l.LinkID == id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        //
        // POST: /Link/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Link link = db.Links.Single(l => l.LinkID == id);
            db.Links.DeleteObject(link);
            db.SaveChanges();
            return RedirectToAction("Content", "Admin", new { id = link.CatID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}