using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nata.Models
{
    public partial class Contracts
    {
        public Contracts()
        {
            Tickets = new HashSet<Tickets>();
        }

        public int Id { get; set; }
        [Display(Name = "Reference Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Account")]
        [Required]
        public int AccountId { get; set; }
        [Display(Name = "Contract Type")]
        [Required]
        public int ContractTypeId { get; set; }
        [Display(Name = "Valid From")]
        [Required]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Valid To")]
        [Required]
        public DateTime DateTo { get; set; }
        [Display(Name = "Efforts (Hours)")]
        [Required]
        public short Hours { get; set; }
        [Display(Name = "Status")]
        [Required]
        public bool Status { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual ContractTypes ContractType { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
