using System;
using System.Collections.Generic;

namespace nata.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Accounts = new HashSet<Accounts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}
