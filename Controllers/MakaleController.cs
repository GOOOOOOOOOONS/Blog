using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    using mBlog.Models;
    public class MakaleController : Controller
    {
        mBlog context = new mBlog();

        // GET: Makale
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detay(int id)
        {
            var data = context.Makales.FirstOrDefault(x => x.MakaleId == id);
            data.GoruntulenmeSayisi++;
            context.SaveChanges();
            return View(data);
        }

        public ActionResult LastKatMakale()
        {
            var model = context.Makales.OrderBy(x => x.GoruntulenmeSayisi).Take(3).ToList();
            return View(model);
        }


        [AllowAnonymous]
        public string Like(int id)
        {
            Makale mk = context.Makales.FirstOrDefault(x => x.MakaleId == id);
            mk.Begeni++;
            context.SaveChanges();
            return mk.Begeni.ToString();
        }
        


        public JsonResult YorumYap(string name, string yorum, string mail, int makalesid)
        {

            var ad = name;
            var yor = yorum;
            var email = mail;

            if (yorum == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            context.Yorums.Add(new Yorum { YorumAdSoyad = ad, YorumAciklama = yor, YorumMail = email, YorumEklenmeTarihi = DateTime.Now, MakaleID = makalesid });

            context.SaveChanges();


            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult YorumSil(int id)
        {
            var yaz = Session["WriterId"];
            var yorum = context.Yorums.Where(x => x.YorumId == id).SingleOrDefault();
            var makale = context.Makales.Where(y => y.MakaleId == yorum.MakaleID).SingleOrDefault();
            if (yaz != null)
            {
                context.Yorums.Remove(yorum);
                context.SaveChanges();
                return RedirectToAction("PostDetail", "Home", new { id = makale.MakaleId });
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}