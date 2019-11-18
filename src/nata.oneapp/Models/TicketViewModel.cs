using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nata.Models
{
    public class TicketViewModel
    {
        public Tickets Ticket { get; set; }
        public string AssignedToUsername { get; set; }
        public string CreatedByUsername { get; set; }
    }
}
