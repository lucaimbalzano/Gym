using Gym.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Lib
{
    public abstract class BaseService
    {
        protected readonly gymEntities db;

        protected BaseService(gymEntities db)
        {
            this.db = db;
        }
    }
}
