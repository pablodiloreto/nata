using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nata.Models
{
    public partial class Accounts
    {
        public Accounts()
        {
            AccountContacts = new HashSet<AccountContacts>();
            Contracts = new HashSet<Contracts>();
        }

        public int Id { get; set; }
        [Display(Name = "Account Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        public string Phone { get; set; }
        [Display(Name = "Location")]
        [Required]
        public int CountryId { get; set; }
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Status")]
        [Required]
        public bool Status { get; set; }

        public virtual Countries Country { get; set; }
        public virtual ICollection<AccountContacts> AccountContacts { get; set; }
        public virtual ICollection<Contracts> Contracts { get; set; }
    }
}
