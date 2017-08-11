using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;
using PagedList;
using PagedList.Mvc;


namespace mBlog.Controllers
{
    using mBlog.Models;
    public class AdminMakalesController : Controller
    {
        mBlog db = new mBlog();

        // GET: AdminMakales
        public ActionResult Index(int Page = 1)
        {
            var makales = db.Makales.Include(m => m.Kategori).Include(m => m.Yazar);
            return View(makales.ToList().ToPagedList(Page, 10));
        }

        // GET: AdminMakales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makale makale = db.Makales.Find(id);
            if (makale == null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }

        // GET: AdminMakales/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");
            ViewBag.YazarID = new SelectList(db.Yazars, "YazarId", "YazarAdi");
            return View();
        }

        // POST: AdminMakales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MakaleId,MakaleBaslik,MakaleIcerik,MakaleEklenmeTarihi,Video,GoruntulenmeSayisi,Begeni,YazarID,KategoriID")] Makale makale, HttpPostedFileBase Foto, string etiketler)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;

                    img.Resize(740, 420);

                    img.Save("~/Assets/Uploads/PostImage/" + newfoto);
                    makale.MakaleResim = "/Assets/Uploads/PostImage/" + newfoto;
                }
                if (etiketler != null)
                {
                    string[] etiketdizi = etiketler.Split(',');
                    foreach (var i in etiketdizi)
                    {
                        var yenietiket = new Etiket { EtiketAdi = i };
                        db.Etikets.Add(yenietiket);
                        makale.Etikets.Add(yenietiket);
                    }
                }
                makale.MakaleEklenmeTarihi = DateTime.Now;
                makale.GoruntulenmeSayisi = 1;
                makale.YazarID = Convert.ToInt32(Session["WriterID"]);                
                makale.Begeni = 0;
                db.Makales.Add(makale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi", makale.KategoriID);
            ViewBag.YazarID = new SelectList(db.Yazars, "YazarId", "YazarAdi", makale.YazarID);
            return View(makale);
        }

        // GET: AdminMakales/Edit/5
        public ActionResult Edit(int? id)
        {
            var makale = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (makale == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi", makale.KategoriID);
            ViewBag.YazarID = new SelectList(db.Yazars, "YazarId", "YazarAdi", makale.YazarID);
            return View(makale);
        }

        // POST: AdminMakales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Makale makale,HttpPostedFileBase Foto,int id)
        {
            try
            {
                var makales = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();
                if (Foto!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(makales.MakaleResim)))
                    {
                        System.IO.File.Delete(Server.MapPath(makales.MakaleResim));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;

                    img.Resize(740, 420);

                    img.Save("~/Assets/Uploads/PostImage/" + newfoto);
                    makales.MakaleResim = "/Assets/Uploads/PostImage/" + newfoto;
                }
                    makales.MakaleBaslik = makale.MakaleBaslik;
                    makales.MakaleIcerik = makale.MakaleIcerik;
                    makales.KategoriID = makale.KategoriID;
                    makales.MakaleEklenmeTarihi = DateTime.Now;
                    ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi", makale.KategoriID);
                    ViewBag.YazarID = new SelectList(db.Yazars, "YazarId", "YazarAdi", makale.YazarID);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            catch (Exception)
            {               
                ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi", makale.KategoriID);
                return View(makale);
            }
              
        }

        // GET: AdminMakales/Delete/5
        public ActionResult Delete(int? id)
        {
            var makale = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();

            if (makale==null)
            {
                return HttpNotFound();
            }

            return View(makale);
        }

        // POST: AdminMakales/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,FormCollection collection)
        {
            try
            {
                var makales = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();
                if (makales==null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(makales.MakaleResim)))
                {
                    System.IO.File.Delete(Server.MapPath(makales.MakaleResim));
                }

                foreach (var i in makales.Yorums.ToList())
                {
                    db.Yorums.Remove(i);
                }

                foreach (var i in makales.Etikets.ToList())
                {
                    db.Etikets.Remove(i);
                }

                db.Makales.Remove(makales);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
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
