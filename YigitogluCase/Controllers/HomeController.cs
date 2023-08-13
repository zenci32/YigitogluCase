using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YigitogluCase.Models;
using YigitogluCase.App_Classes;

namespace YigitogluCase.Controllers
{
    public class HomeController : Controller
    {
        YigitOgluContext context = new YigitOgluContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakaleListele() {
            var data = context.Makales.ToList();
            return View("MakaleListeleWidget", data);
        }
        public PartialViewResult PopulerMakalelerWidget() {
            var model = context.Makales.OrderByDescending(x => x.EklenmeTarihi).Take(3).ToList();
            return PartialView(model);
        }
    }
}