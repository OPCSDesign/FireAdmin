using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult index()
        {
            return View();
        }

        public ActionResult about()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}