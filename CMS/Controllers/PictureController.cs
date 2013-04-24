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
    public class PictureController : Controller
    {
        private cms_2Entities db = new cms_2Entities();

        //
        // GET: /Picture/

        public ActionResult Index()
        {
            var pictures = db.Pictures.Include("Category");
            return View(pictures.ToList());
        }

        //
        // GET: /Picture/Details/5

        public ActionResult Details(long id = 0)
        {
            Picture picture = db.Pictures.Single(p => p.PicID == id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        //
        // GET: /Picture/Create

        public ActionResult Create(int? id)
        {

            ViewBag.CatID = id;
            var category = db.Categories.First(c => c.CatID == id.Value);
            ViewBag.CatName = category.CatName;

            return View();
        }

        //
        // POST: /Picture/Create

        [HttpPost]
        public ActionResult Create(Picture picture)
        {
            if (ModelState.IsValid)
            {
                db.Pictures.AddObject(picture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", picture.CatID);
            return View(picture);
        }

        //
        // GET: /Picture/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Picture picture = db.Pictures.Single(p => p.PicID == id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", picture.CatID);
            return View(picture);
        }

        //
        // POST: /Picture/Edit/5

        [HttpPost]
        public ActionResult Edit(Picture picture)
        {
            if (ModelState.IsValid)
            {
                db.Pictures.Attach(picture);
                db.ObjectStateManager.ChangeObjectState(picture, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", picture.CatID);
            return View(picture);
        }

        //
        // GET: /Picture/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Picture picture = db.Pictures.Single(p => p.PicID == id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        //
        // POST: /Picture/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Picture picture = db.Pictures.Single(p => p.PicID == id);
            db.Pictures.DeleteObject(picture);
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