using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMBDatabase.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Part()
        {
            ViewBag.Message = "Parts page.";

            return View();
        }


        public ActionResult Electronics()
        {
            ViewBag.Message = "Electronics page.";

            return View();
        }
    }
}