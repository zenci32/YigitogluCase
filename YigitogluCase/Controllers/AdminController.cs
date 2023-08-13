using System.Linq;
using System.Web.Mvc;
using YigitogluCase.Models;

namespace YigitogluCase.Controllers
{

    public class AdminController : Controller
    {

        // GET: Admin
        YigitOgluContext context = new YigitOgluContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult YazarOnaylari()
        {
            var data = context.Kullanicis.Where(x => x.Yazar == true && x.Onaylandi == false).ToList();
            return View(data);
        }

        public ActionResult OnayVer(int id)
        {
            Kullanici kl = context.Kullanicis.FirstOrDefault(x => x.KullaniciId == id);
            kl.Onaylandi = true;
            context.SaveChanges();
            return RedirectToAction("YazarOnaylari");
        }
    }
}