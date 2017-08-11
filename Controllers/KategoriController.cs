using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC.Controllers
{
    using mBlog.Models;
    using PagedList;

    public class KategoriController : Controller
    {
        mBlog context = new mBlog();
        // GET: Kategori
        public ActionResult Index(int id)
        {

            return View(id);
        }

        public ActionResult MakaleListele(int id, int Page = 1)
        {
            var data = context.Makales.Where(x => x.KategoriID == id).ToList().ToPagedList(Page, 5); ;
            return View("_MakaleListeleWidget", data);
        }

        public PartialViewResult KategoriWidget()
        {
            return PartialView(context.Kategoris.ToList().Take(10));
        }

        
    }
}