using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Configuration;
namespace AppServiceDemo.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var model = WebConfigurationManager.AppSettings["Greeting"];
            // String path = ConfigurationManager.AppSettings["Greeting"];

            //return View("Index", (string)model);
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
    }
}