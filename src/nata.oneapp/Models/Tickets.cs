using System;
using System.Collections.Generic;

namespace nata.Models
{
    public partial class Tickets
    {
        public Tickets()
        {
            Activities = new HashSet<Activities>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ContractId { get; set; }
        public string AssignedTo { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public int ContactId { get; set; }
        public int TicketTypeId { get; set; }
        public byte? EstimatedHours { get; set; }
        public string CreatedBy { get; set; }
        public int TicketImpactId { get; set; }
        public int TicketUrgencyId { get; set; }
        public byte TicketPriority { get; set; }
        public bool Status { get; set; }

        public virtual Contacts Contact { get; set; }
        public virtual Contracts Contract { get; set; }
        public virtual TicketImpacts TicketImpact { get; set; }
        public virtual TicketTypes TicketType { get; set; }
        public virtual TicketUrgencies TicketUrgency { get; set; }
        public virtual ICollection<Activities> Activities { get; set; }
    }
}
