using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gym.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index","Admin");
            } else if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("Index", "Teacher");
            } else
            {
                return RedirectToAction("Index", "Customer");
            }
        } 
    }
}