using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YigitogluCase.Models;

namespace YigitogluCase.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici

        YigitOgluContext context = new YigitOgluContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(Kullanici kl)
        {
            var ka = ValidateUser(kl.KullaniciAdi, kl.Parola);
            if (ka !=null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, kl.KullaniciAdi, DateTime.Now, DateTime.Now.AddMinutes(15), true, ka.KullaniciAdi, FormsAuthentication.FormsCookiePath);

                HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName);
                Response.Cookies.Add(ck);
                Session["UserID"] = ka.KullaniciId;

                //if (ticket.IsPersistent)
                //{
                //    ck.Expires = ticket.Expiration;
                //}

                // Response.Redirect(FormsAuthentication.GetRedirectUrl(kl.KullaniciAdi, true));
                FormsAuthentication.RedirectFromLoginPage(kl.KullaniciAdi, true);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("GirisYap");
        }

        Kullanici ValidateUser(string ka, string pwd)
        {
            Kullanici kl = context.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == ka && x.Parola == pwd);
            if (kl != null)
                return kl;
            else
            {
                return null;
            }
        }

        public ActionResult CikisYap(string redirectUrl)
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            return Redirect(redirectUrl);
        }

        public ActionResult UyeOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeOl(Kullanici kl , string rdBay , string rdBayan)
        {
            if (!string.IsNullOrEmpty(rdBay))
                kl.Cinsiyet = true;
            if (!string.IsNullOrEmpty(rdBayan))
                kl.Cinsiyet = false;

            kl.Yazar = false;
            kl.Onaylandi = true;
            kl.Aktif = true;
            kl.KayitTarihi = DateTime.Now;
            context.Kullanicis.Add(kl);
            context.SaveChanges();

            Rol yazar = context.Rols.FirstOrDefault(x => x.RolAdi == "Üye");

            KullaniciRol kr = new KullaniciRol();
            kr.RolID = yazar.RolID;
            kr.KullaniciID = kl.KullaniciId;
            context.KullaniciRols.Add(kr);
            context.SaveChanges();

            return RedirectToAction("GirisYap");
        }

    }
}