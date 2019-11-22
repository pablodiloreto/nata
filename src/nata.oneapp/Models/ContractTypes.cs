using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nata.Models
{
    public partial class ContractTypes
    {
        public ContractTypes()
        {
            Contracts = new HashSet<Contracts>();
        }

        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Hours required?")]
        [Required]
        public bool HoursRequired { get; set; }
        [Display(Name = "Status")]
        [Required]
        public bool Status { get; set; }

        public virtual ICollection<Contracts> Contracts { get; set; }
    }
}
