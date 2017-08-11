using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC.Controllers
{
    using mBlog.Models;
    using PagedList;

    public class EtiketController : Controller
    {
        mBlog context = new mBlog();
        // GET: Etiket
        public ActionResult Index(int id)
        {
            return View(id);
        }
        public PartialViewResult EtiketlerWidget()
        {
            return PartialView(context.Etikets.ToList().Take(10));

        }

        public ActionResult MakaleListele(int id, int Page = 1)
        {
            var data = context.Makales.Where(x => x.Etikets.Any(y => y.EtiketId == id)).ToList().ToPagedList(Page, 5);
            return View("_MakaleListeleWidget", data);
        }
    }
}