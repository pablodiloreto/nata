using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nata.Models
{
    public partial class Activities
    {
        public int Id { get; set; }
        [Display(Name = "Ticket")]
        [Required]
        public int TicketId { get; set; }
        [Display(Name = "Date of Activity")]
        [Required]
        public DateTime Date { get; set; }
        [Display(Name = "User")]
        [Required]
        public string UserId { get; set; }
        [Display(Name = "Efforts")]
        public decimal? Efforts { get; set; }
        [Display(Name = "Details")]
        public string Details { get; set; }

        public virtual Tickets Ticket { get; set; }
    }
}
