using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace mBlog.Controllers
{
    using mBlog.Models;
    using PagedList;
    using System.Data.Entity;
    using System.Net;
    using System.Web.Helpers;

    public class YazarController : Controller
    {   
        // GET: Yazar
        mBlog context = new mBlog();
        // GET: User
        public ActionResult Index(int id)
        {
            return View();
        }
        public ActionResult Detay(int id)
        {
            return View(id);
        }

        public ActionResult Profile(int id)
        {
            var data = context.Yazars.Where(x => x.YazarId == id).SingleOrDefault();           
            return View(data);    

        }



        public ActionResult MakaleListele(int id ,int Page = 1)
        {
            var data = context.Makales.Where(x=>x.YazarID==id).ToList().ToPagedList(Page, 5);
            return View("_MakaleListeleWidget", data);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Yazar uye, string YazarSifre)
        {
          

            var login = context.Yazars.Where(x => x.YazarKullaniciAdi == uye.YazarKullaniciAdi).SingleOrDefault();
            if (login.YazarKullaniciAdi==uye.YazarKullaniciAdi&&login.YazarSifre== YazarSifre)
            {
                Session["WriterID"] = login.YazarId;
                Session["WriterKadi"] = login.YazarKullaniciAdi;
                Session["WriterName"] = login.YazarAdi;
                Session["WriterSurname"] = login.YazarSoyad;
                Session["AuthorisationID"] = login.YetkiID;
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Uyari = "Kullanıcı Adı yada Sifrenizi Kontrol Ediniz!";
                return View("Login");
            }
                
           
        }

        public ActionResult Logout()
        {
            Session["WriterID"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


      

    }
}