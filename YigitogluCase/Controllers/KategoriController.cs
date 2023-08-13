using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YigitogluCase.Models;

namespace YigitogluCase.Controllers
{
    public class KategoriController : Controller
    {
        YigitOgluContext context = new YigitOgluContext();
        // GET: Kategori
        public ActionResult Index(int id)
        {
            return View(id);
        }

        public PartialViewResult KategoriWidget()
        {

            return PartialView(context.Kategoris.ToList());
        }

        public ActionResult MakaleListele(int id)
        {
            var data = context.Makales.Where(x => x.KategoriID == id).ToList();
            return View("MakaleListeleWidget",data);
        }
    }
}