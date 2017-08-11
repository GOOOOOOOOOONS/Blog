using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace mBlog.Controllers
{
    using mBlog.Models;
    using PagedList;

    public class AdminYazarsController : Controller
    {
        private mBlog db = new mBlog();

        // GET: AdminYazars
        public ActionResult Index(int Page = 1)
        {
            var yazars = db.Yazars.Include(y => y.YazarYetkis);
            return View(yazars.ToList().ToPagedList(Page, 10));
        }

        // GET: AdminYazars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yazar yazar = db.Yazars.Find(id);
            if (yazar == null)
            {
                return HttpNotFound();
            }
            return View(yazar);
        }

        // GET: AdminYazars/Create
        public ActionResult Create()
        {
            ViewBag.YetkiID = new SelectList(db.Yetkis, "YetkiId", "YetkiAdi");
            return View();
        }

        // POST: AdminYazars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YazarId,YazarAdi,YazarSoyad,YazarKullaniciAdi,YazarSifre,YazarMailAdres,YazarAciklama,YetkiID,YazarFace,YazarInstagram,YazarGithub,YazarLinked,YazarYetki")] Yazar yazar)
        {
            if (ModelState.IsValid)
            {
                yazar.YazarEklenmeTarihi = DateTime.Now;
                db.Yazars.Add(yazar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.YetkiID = new SelectList(db.Yetkis, "YetkiId", "YetkiAdi", yazar.YetkiID);
            return View(yazar);
        }

        // GET: AdminYazars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yazar yazar = db.Yazars.Find(id);
            if (yazar == null)
            {
                return HttpNotFound();
            }
            ViewBag.YetkiID = new SelectList(db.Yetkis, "YetkiId", "YetkiAdi", yazar.YetkiID);
            return View(yazar);
        }

        // POST: AdminYazars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YazarId,YazarAdi,YazarSoyad,YazarKullaniciAdi,YazarSifre,YazarMailAdres,YazarAciklama,YetkiID,YazarFace,YazarInstagram,YazarGithub,YazarLinked,YazarYetki")] Yazar yazar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yazar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.YetkiID = new SelectList(db.Yetkis, "YetkiId", "YetkiAdi", yazar.YetkiID);
            return View(yazar);
        }

        // GET: AdminYazars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yazar yazar = db.Yazars.Find(id);
            if (yazar == null)
            {
                return HttpNotFound();
            }
            return View(yazar);
        }

        // POST: AdminYazars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yazar yazar = db.Yazars.Find(id); 
            db.Yazars.Remove(yazar);
            db.SaveChanges();
            if (Convert.ToInt32(Session["WriterID"]) == id)
            {
                Session["WriterID"] = null;
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }

            else
            {
                if (Convert.ToInt32(Session["AuthorisationID"]) == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["WriterID"] = null;
                    Session.Abandon();
                    return RedirectToAction("Index", "Home");
                }
            }

           
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
