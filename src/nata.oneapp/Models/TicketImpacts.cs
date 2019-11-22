using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nata.Models
{
    public partial class TicketImpacts
    {
        public TicketImpacts()
        {
            Tickets = new HashSet<Tickets>();
        }

        public int Id { get; set; }
        [Display(Name = "Weight")]
        [Required]
        public byte Weight { get; set; }
        [Display(Name = "Reference Name")]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
