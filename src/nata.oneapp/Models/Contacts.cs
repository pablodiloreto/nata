using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<AccountContacts> AccountContacts { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
