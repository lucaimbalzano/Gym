using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gym.Web.Models.Account
{
    public class UserInfoModel
    {
        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }
    }
}