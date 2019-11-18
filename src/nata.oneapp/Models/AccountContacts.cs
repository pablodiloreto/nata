using System;
using System.Collections.Generic;

namespace nata.Models
{
    public partial class AccountContacts
    {
        public int AccountId { get; set; }
        public int ContactId { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual Contacts Contact { get; set; }
    }
}
