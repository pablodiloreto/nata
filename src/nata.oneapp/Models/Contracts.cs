using System;
using System.Collections.Generic;

namespace nata.Models
{
    public partial class Contracts
    {
        public Contracts()
        {
            Tickets = new HashSet<Tickets>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        public int ContractTypeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public short Hours { get; set; }
        public bool Status { get; set; }
        public virtual Accounts Account { get; set; }
        public virtual ContractTypes ContractType { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
