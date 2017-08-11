using System.Linq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;


namespace MVC.Controllers
{
    using mBlog.Models;
    public class HomeController : Controller
    {
        mBlog context = new mBlog();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakaleListele(int Page = 1)
        {
            var data = context.Makales.OrderByDescending(x=>x.MakaleId).ToList().ToPagedList(Page, 7);
            return View("_MakaleListeleWidget", data);
        }

       

        public PartialViewResult PopulerMakalelerWidget()
        {
            var model = context.Makales.OrderByDescending(x => x.GoruntulenmeSayisi).Take(4).ToList();
            return PartialView(model);
        }

        public PartialViewResult LikeMakalelerWidget()
        {
            var model = context.Makales.OrderByDescending(x => x.Begeni).Take(4).ToList();
            return PartialView(model);
        }

        public PartialViewResult NewMakaleler()
        {
            var model = context.Makales.OrderBy(x => x.GoruntulenmeSayisi).Take(4).ToList();
            return PartialView(model);
        }

       

       
      

    }

}