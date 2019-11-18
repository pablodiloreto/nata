using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int CountryId { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        public virtual Countries Country { get; set; }
        public virtual ICollection<AccountContacts> AccountContacts { get; set; }
        public virtual ICollection<Contracts> Contracts { get; set; }
    }
}
