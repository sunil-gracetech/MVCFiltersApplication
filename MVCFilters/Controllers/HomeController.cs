using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MVCFilters.Controllers
{
   // [HandleError]
    public class HomeController : Controller
    {
        [OutputCache(Duration =60, Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            
                ViewBag.now = DateTime.Now;
                throw new Exception("error in application");

          
           return View();
        }

      
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            throw new FormatException("data fromtting error !");
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}