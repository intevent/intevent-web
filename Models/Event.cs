using System;

namespace intevent_web.Models
{
    public class Event
    {
        public int Id { get; set; }

        public DateTimeOffset? TimeStarted { get; set; }
    }
}
