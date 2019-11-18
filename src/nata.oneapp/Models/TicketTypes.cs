using System;
using System.Collections.Generic;

namespace nata.Models
{
    public partial class TicketTypes
    {
        public TicketTypes()
        {
            Tickets = new HashSet<Tickets>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
