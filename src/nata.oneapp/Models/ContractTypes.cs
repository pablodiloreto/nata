using System;
using System.Collections.Generic;

namespace nata.Models
{
    public partial class ContractTypes
    {
        public ContractTypes()
        {
            Contracts = new HashSet<Contracts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool HoursRequired { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Contracts> Contracts { get; set; }
    }
}
