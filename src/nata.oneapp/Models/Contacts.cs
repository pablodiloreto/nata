using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nata.Models
{
    public partial class Contacts
    {
        public Contacts()
        {
            AccountContacts = new HashSet<AccountContacts>();
            Tickets = new HashSet<Tickets>();
        }

        public int Id { get; set; }

        [Display(Name = "Contact Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        public string Phone { get; set; }
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }

        public virtual ICollection<AccountContacts> AccountContacts { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
