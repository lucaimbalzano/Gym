using Gym.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Lib.Services.Customers
{
    public class SubscriptionService : BaseService
    {
        public SubscriptionService(gymEntities db) : base(db)
        {
        }

        public IList<Subscription> GetSubscriptionsList()
        {
            return db.Subscription.OrderBy(x => x.Duration).ToList();
        }

        public Subscription GetSubscriptionByName(string subscription)
        {
            return db.Subscription.FirstOrDefault(x => x.Name == subscription);
        }
    }
}
