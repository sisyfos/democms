﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using System.IO;
using CSharpVitamins;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private cms_2Entities db = new cms_2Entities();

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult SiteInfo()
        {
            var siteconfig = db.SiteConfigs.First();

            ViewBag.SiteName = siteconfig.SiteName;
            ViewBag.SiteDesc = siteconfig.SiteDesc;

            return PartialView();

        }

        [ChildActionOnly]
        public ActionResult ShowStartPage()
        {
            var startpage = db.SiteConfigs.First();
            var startid = startpage.SiteStartCatID;

            var category = new Category();

            if (startid.HasValue)
            {
                category = db.Categories.Include("Texts").Include("Videos").Include("Links").Include("Pictures").First(c => c.CatID == startid.Value);
            }            

            return PartialView(category);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Imagenew()
        {
            var model = new ImageModel()
            {
                Images = Directory.EnumerateFiles(Server.MapPath("~/images_upload/"))
                .Select(fn => "~/images_upload/" + Path.GetFileName(fn))
            };
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult test()
        {
            var model = new ImageModel()
            {
                Images = Directory.EnumerateFiles(Server.MapPath("~/images_upload/"))
                .Select(fn => "~/images_upload/" + Path.GetFileName(fn))
            };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Imagenew(ImageModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = ShortGuid.NewGuid().ToString();
                string serverPath = Server.MapPath("~");
                string imagesPath = serverPath + "/images_upload";
                string thumbPath = imagesPath + "/images_upload";
                string fullPath = imagesPath + "/images_upload";
                ImageModel.ResizeAndSave(thumbPath, fileName, model.ImageUploaded.InputStream, 80, true);
                ImageModel.ResizeAndSave(fullPath, fileName, model.ImageUploaded.InputStream, 600, true);
            }
            return RedirectToAction("Imagenew");
        }

    }
}
