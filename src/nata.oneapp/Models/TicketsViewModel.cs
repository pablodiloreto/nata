using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace nata.Models
{
    public class TicketsViewModel
    {
        public List<Tickets> Tickets { get; set; }
        public SelectList Accounts { get; set; }
        public SelectList Status { get; set; }
        public SelectList Users { get; set; }
        public SelectList Priority { get; set; }
        public Tickets Ticket { get; set; }
        public string SearchStatus { get; set; }
        public string SearchClient { get; set; }
        public string SearchUsername { get; set; }
        public string SearchString { get; set; }
        public string SearchPriority { get; set; }
        public string AssignedToUserName { get; set; }
        public string CreatedByUserName { get; set; }
        public string Account { get; set; }
    }
}
