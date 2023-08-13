using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YigitogluCase.Models;
using YigitogluCase.App_Classes;
using System.Drawing;
using System.IO;

namespace YigitogluCase.Controllers
{
    [Authorize]
    public class MakaleController : Controller
    {
        YigitOgluContext context = new YigitOgluContext();
        // GET: Makale
        [AllowAnonymous]
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                var data = context.Makales.Where(x => x.YazarID == id).ToList();
                if (data != null)
                    return View(data);
            }

            var adminData = context.Makales.ToList();
            return View(adminData);
        }

        [AllowAnonymous]
        public ActionResult Detay(int id)
        {
            var data = context.Makales.FirstOrDefault(x => x.MakaleId == id);

            if (Session["UserID"] != null)
            {
                var kullaniciId = Convert.ToInt32(Session["UserID"].ToString());
                var yazarTakipCheck = context.YazarTakips.FirstOrDefault(x => x.YazarId == data.YazarID && x.KullaniciId == kullaniciId);
                if (yazarTakipCheck != null)
                {
                    Session["TakipCheck"] = 1;
                }
                else
                {
                    Session["TakipCheck"] = 0;
                }
            }
            return View(data);
        }

        [AllowAnonymous]
        public string Begen(int id)
        {
            Makale mkl = context.Makales.FirstOrDefault(x => x.MakaleId == id);
            mkl.Begeni++;
            context.SaveChanges();
            return mkl.Begeni.ToString();

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult YazarTakip(int? id)
        {
            var kullaniciId = Convert.ToInt32(Session["UserID"].ToString());
            var yazarId = context.Makales.FirstOrDefault(x => x.MakaleId == id).YazarID;
            var yzrtkp = new YazarTakip { KullaniciId = kullaniciId, YazarId = yazarId };
            context.YazarTakips.Add(yzrtkp);
            context.SaveChanges();
            return RedirectToAction("Detay", "Makale", new { @id = id });
        }

        [AllowAnonymous]
        public string Goruntulendi(int id)
        {

            Makale mkl = context.Makales.FirstOrDefault(x => x.MakaleId == id);
            mkl.GoruntulenmeSayisi++;
            context.SaveChanges();
            return mkl.GoruntulenmeSayisi.ToString();
        }

        [Authorize(Roles = "Admin, Yazar")]
        public ActionResult MakaleEkle()
        {
            ViewBag.Kategoriler = context.Kategoris.ToList();
            return View();
        }

        [Authorize(Roles = "Admin, Yazar")]
        [HttpPost]
        public ActionResult MakaleEkle(Makale mkl, HttpPostedFileBase resim)
        {
            Image img = Image.FromStream(resim.InputStream);
            Bitmap kckResim = new Bitmap(img, Settings.ResimKucukBoyut);
            Bitmap ortResim = new Bitmap(img, Settings.ResimOrtaBoyut);
            Bitmap bykResim = new Bitmap(img, Settings.ResimBuyukBoyut);

            kckResim.Save(Server.MapPath("/Content/MakaleResim/KucukBoyut/" + resim.FileName));
            ortResim.Save(Server.MapPath("/Content/MakaleResim/OrtaBoyut/" + resim.FileName));
            bykResim.Save(Server.MapPath("/Content/MakaleResim/BuyukBoyut/" + resim.FileName));

            Resim rsm = new Resim();
            rsm.KucukBoyut = "/Content/MakaleResim/KucukBoyut/" + resim.FileName;
            rsm.OrtaBoyut = "/Content/MakaleResim/OrtaBoyut/" + resim.FileName;
            rsm.BuyukBoyut = "/Content/MakaleResim/BuyukBoyut/" + resim.FileName;

            context.Resims.Add(rsm);
            context.SaveChanges();

            mkl.ResimID = rsm.ResimId;
            mkl.EklenmeTarihi = DateTime.Now;
            mkl.GoruntulenmeSayisi = 0;
            mkl.Begeni = 0;

            int yzrId = context.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name).KullaniciId;
            mkl.YazarID = yzrId;

            context.Makales.Add(mkl);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult YorumEkle(Yorum yorum)
        {

            yorum.EklenmeTarihi = DateTime.Now;
            context.Yorums.Add(yorum);
            context.SaveChanges();

            return RedirectToAction("Detay", "Makale", new { @id = yorum.MakaleID });
        }
        public ActionResult MakaleGetir(int id)
        {
            ViewBag.Kategoriler = context.Kategoris.ToList();
            var data = context.Makales.Find(id);
            return View("MakaleGetir", data);
        }
        [HttpPost]
        public ActionResult MakaleGuncelle(Makale makale, HttpPostedFileBase resim)
        {
            Resim rsm = new Resim();
            var kullaniciId = Convert.ToInt32(Session["UserID"].ToString());
            string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);

            if (Request.Files.Count > 0)
            {
                if (dosyaAdi != string.Empty)
                {
                    Image img = Image.FromStream(resim.InputStream);
                    Bitmap kckResim = new Bitmap(img, Settings.ResimKucukBoyut);
                    Bitmap ortResim = new Bitmap(img, Settings.ResimOrtaBoyut);
                    Bitmap bykResim = new Bitmap(img, Settings.ResimBuyukBoyut);

                    kckResim.Save(Server.MapPath("/Content/MakaleResim/KucukBoyut/" + resim.FileName));
                    ortResim.Save(Server.MapPath("/Content/MakaleResim/OrtaBoyut/" + resim.FileName));
                    bykResim.Save(Server.MapPath("/Content/MakaleResim/BuyukBoyut/" + resim.FileName));

                    
                    rsm.KucukBoyut = "/Content/MakaleResim/KucukBoyut/" + resim.FileName;
                    rsm.OrtaBoyut = "/Content/MakaleResim/OrtaBoyut/" + resim.FileName;
                    rsm.BuyukBoyut = "/Content/MakaleResim/BuyukBoyut/" + resim.FileName;

                    context.Resims.Add(rsm);
                    context.SaveChanges();
                }
            }

            var mkl = context.Makales.Find(makale.MakaleId);
            if (dosyaAdi != string.Empty)
                mkl.ResimID = rsm.ResimId;
            mkl.Icerik = makale.Icerik;
            mkl.KategoriID = makale.KategoriID;
            mkl.Baslik = makale.Baslik;

            context.SaveChanges();
            return RedirectToAction("Index", "Makale", new { @id = kullaniciId });
        }
        public ActionResult MakaleSil(int? id)
        {
            var kullaniciId = Convert.ToInt32(Session["UserID"].ToString());
            var makale = context.Makales.Find(id);
           
            context.Makales.Remove(makale);
            context.SaveChanges();

            return RedirectToAction("Index", "Makale", new {@id = kullaniciId});
        }
    }
}