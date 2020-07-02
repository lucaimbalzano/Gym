using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gym.Web.Controllers
{
    public abstract class AbstractBaseController : Controller
    {
        public bool IsAuthenticated => User.Identity.IsAuthenticated;

        public int UserId => int.Parse(User.Identity.Name);
    }
}