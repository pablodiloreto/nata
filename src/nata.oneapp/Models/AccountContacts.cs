using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nata.Models
{
    public partial class AccountContacts
    {
        [Display(Name = "Account")]
        [Required]
        public int AccountId { get; set; }
        [Display(Name = "Contact")]
        [Required]
        public int ContactId { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual Contacts Contact { get; set; }
    }
}
