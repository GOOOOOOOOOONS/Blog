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

    public class AdminYorumsController : Controller
    {
        private mBlog db = new mBlog();

        // GET: AdminYorums
        public ActionResult Index(int Page = 1)
        {
            var yorums = db.Yorums.Include(y => y.Makale);
            return View(yorums.ToList().ToPagedList(Page, 10));
        }

        // GET: AdminYorums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // GET: AdminYorums/Create
        public ActionResult Create()
        {
            ViewBag.MakaleID = new SelectList(db.Makales, "MakaleId", "MakaleBaslik");
            return View();
        }

        // POST: AdminYorums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YorumId,YorumAciklama,MakaleID,YorumEklenmeTarihi,YorumAdSoyad,YorumMail,YorumBegeni")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Yorums.Add(yorum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MakaleID = new SelectList(db.Makales, "MakaleId", "MakaleBaslik", yorum.MakaleID);
            return View(yorum);
        }

        // GET: AdminYorums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakaleID = new SelectList(db.Makales, "MakaleId", "MakaleBaslik", yorum.MakaleID);
            return View(yorum);
        }

        // POST: AdminYorums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YorumId,YorumAciklama,MakaleID,YorumEklenmeTarihi,YorumAdSoyad,YorumMail,YorumBegeni")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yorum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MakaleID = new SelectList(db.Makales, "MakaleId", "MakaleBaslik", yorum.MakaleID);
            return View(yorum);
        }

        // GET: AdminYorums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // POST: AdminYorums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yorum yorum = db.Yorums.Find(id);
            db.Yorums.Remove(yorum);
            db.SaveChanges();
            return RedirectToAction("Index");
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
