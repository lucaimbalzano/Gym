//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gym.Lib.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subscription
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subscription()
        {
            this.CustomerSubscription = new HashSet<CustomerSubscription>();
        }
    
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string Note { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerSubscription> CustomerSubscription { get; set; }
    }
}
