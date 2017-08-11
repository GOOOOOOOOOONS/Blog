using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC.Controllers
{
    using mBlog.Models;
    using System.Web.Helpers;

    public class AdminController : Controller
    {

        mBlog context = new mBlog();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.MakaleSayisi = context.Makales.Count();
            ViewBag.YazarSayisi = context.Yazars.Count();
            ViewBag.YorumSayisi = context.Yorums.Count();
            ViewBag.EtiketSayisi = context.Etikets.Count();
            return View();
        }      
        
    }
}