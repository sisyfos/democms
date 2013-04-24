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
    [Authorize]
    public class AdminController : Controller
    {
        private cms_2Entities db = new cms_2Entities();

// -- SiteConfig --

        public ActionResult Index()
        {
            var siteconfig = db.SiteConfigs.First();
            ViewBag.SiteConfigID = siteconfig.SiteConfigID;
            ViewBag.SiteName = siteconfig.SiteName;
            ViewBag.SiteDesc = siteconfig.SiteDesc;
            ViewBag.CurrentSiteStartCatID = siteconfig.SiteStartCatID;
            ViewBag.SiteStartCatID = new SelectList(db.Categories, "CatID", "CatName");
            return View();
        }

        [HttpPost]
        public ActionResult Siteconfig(SiteConfig siteconfig)
        {
            if (ModelState.IsValid)
            {
                db.SiteConfigs.Attach(siteconfig);
                db.ObjectStateManager.ChangeObjectState(siteconfig, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Page");
            }
            ViewBag.SiteStartCatID = new SelectList(db.Categories, "CatID", "CatName", siteconfig.SiteStartCatID);
            return View(siteconfig);
        }

// -- Page --

        public ActionResult Page()
        {
            var categories = db.Categories.Include("Template");
            return View(categories.ToList());
        }
        
        public ActionResult EditPage(long id = 0)
        {
            Category category = db.Categories.Single(c => c.CatID == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.TempID = new SelectList(db.Templates, "TempID", "TempName", category.TempID);
            return View(category);
        }

        public ActionResult DeletePage(long id = 0)
        {
            Category category = db.Categories.Single(c => c.CatID == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("DeletePage")]
        public ActionResult DeleteConfirmed(long id)
        {
            Category category = db.Categories.Single(c => c.CatID == id);
            db.Categories.DeleteObject(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreatePage(Category model)
        {
            model.TempID = 1;
            var redirectController = new CategoryController();
            return redirectController.Create(model);
        }

// -- Content --

        public ActionResult Content(int? id)
        {
            var category = new Category();

            if (id.HasValue)
            {
                category = db.Categories.First(c => c.CatID == id.Value);
            }
            else
            {
                return RedirectToAction("ContentNoID");
            }

            return View(category);
        }

        public ActionResult ContentNoID()
        {
            var categories = db.Categories.Include("Template");
            return View(categories.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

