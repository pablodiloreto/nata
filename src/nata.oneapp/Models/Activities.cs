using System;
using System.Collections.Generic;

namespace nata.Models
{
    public partial class Activities
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public decimal? Efforts { get; set; }
        public string Details { get; set; }

        public virtual Tickets Ticket { get; set; }
    }
}
