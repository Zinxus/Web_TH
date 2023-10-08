using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Model;
namespace Web_TH.Controllers
{
    public class siteController : Controller
    {
        // GET: site
        public ActionResult Index()
        {
            return View();
        }
    }
}