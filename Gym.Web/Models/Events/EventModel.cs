using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace Gym.Web.Models.Events
{
    public class EventModel
    {
        public string id { get; set; }

        public string title { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public bool allDay => end == null;
    }
}