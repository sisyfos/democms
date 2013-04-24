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
    public class VideoController : Controller
    {
        private cms_2Entities db = new cms_2Entities();

        //
        // GET: /Video/

        public ActionResult Index()
        {
            var videos = db.Videos.Include("Category");
            return View(videos.ToList());
        }

        //
        // GET: /Video/Details/5

        public ActionResult Details(long id = 0)
        {
            Video video = db.Videos.Single(v => v.VidID == id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        //
        // GET: /Video/Create

        public ActionResult Create(int? id)
        {

            ViewBag.CatID = id;
            var category = db.Categories.First(c => c.CatID == id.Value);
            ViewBag.CatName = category.CatName;

            return View();
        }

        //
        // POST: /Video/Create

        [HttpPost]
        public ActionResult Create(Video video)
        {
            if (ModelState.IsValid)
            {
                Uri uri = new Uri(video.VidUrl);
                video.VidUrl = HttpUtility.ParseQueryString(uri.Query).Get("v");                
                db.Videos.AddObject(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", video.CatID);
            return View(video);
        }

        //
        // GET: /Video/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Video video = db.Videos.Single(v => v.VidID == id);
            if (video == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", video.CatID);
            return View(video);
        }

        //
        // POST: /Video/Edit/5

        [HttpPost]
        public ActionResult Edit(Video video)
        {
            if (ModelState.IsValid)
            {
                /*
                Uri uri = new Uri(video.VidUrl);
                video.VidUrl = HttpUtility.ParseQueryString(uri.Query).Get("v");  
                */
                db.Videos.Attach(video);
                db.ObjectStateManager.ChangeObjectState(video, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName", video.CatID);
            return View(video);
        }

        //
        // GET: /Video/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Video video = db.Videos.Single(v => v.VidID == id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        //
        // POST: /Video/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Video video = db.Videos.Single(v => v.VidID == id);
            db.Videos.DeleteObject(video);
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