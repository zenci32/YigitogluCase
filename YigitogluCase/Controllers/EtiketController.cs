using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YigitogluCase.Models;

namespace YigitogluCase.Controllers
{
  
    public class EtiketController : Controller
    {
        YigitOgluContext context = new YigitOgluContext();
        // GET: Etiket
        public ActionResult Index(int id)
        {
            return View(id);
        }

        public PartialViewResult EtiketWidget()
        {

            return PartialView(context.Etikets.ToList());
        }

        public ActionResult MakaleListele(int id)
        {
            var data = context.Makales.Where(x => x.Etikets.Any(y => y.EtiketId == id)).ToList();
            return View("MakaleListeleWidget" , data);
        }
    }
}