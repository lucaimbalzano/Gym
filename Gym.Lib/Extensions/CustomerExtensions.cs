using Gym.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Lib.Extensions
{
    public static class CustomerExtensions
    {
        public static bool IsUnder20(this Customer customer)
        {
            return ((DateTime.Now - customer.BirthDate).TotalDays / 365) <= 20;
        }
    }
}
