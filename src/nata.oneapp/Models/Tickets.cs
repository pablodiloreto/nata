using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nata.Models
{
    public partial class Tickets
    {
        public Tickets()
        {
            Activities = new HashSet<Activities>();
        }

        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Contract")]
        [Required]
        public int ContractId { get; set; }
        [Display(Name = "Assigned To")]
        [Required]
        public string AssignedTo { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Opened From")]
        [Required]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Contact for Ticket")]
        [Required]
        public int ContactId { get; set; }
        [Display(Name = "Ticket Type")]
        [Required]
        public int TicketTypeId { get; set; }
        [Display(Name = "Estimated Effort")]
        public byte? EstimatedHours { get; set; }
        [Display(Name = "Created By")]
        [Required]
        public string CreatedBy { get; set; }
        [Display(Name = "Impact")]
        [Required]
        public int TicketImpactId { get; set; }
        [Display(Name = "Urgency")]
        [Required]
        public int TicketUrgencyId { get; set; }
        [Display(Name = "Priority")]
        [Required]
        public byte TicketPriority { get; set; }
        [Display(Name = "Status")]
        [Required]
        public bool Status { get; set; }

        public virtual Contacts Contact { get; set; }
        public virtual Contracts Contract { get; set; }
        public virtual TicketImpacts TicketImpact { get; set; }
        public virtual TicketTypes TicketType { get; set; }
        public virtual TicketUrgencies TicketUrgency { get; set; }
        public virtual ICollection<Activities> Activities { get; set; }
    }
}
