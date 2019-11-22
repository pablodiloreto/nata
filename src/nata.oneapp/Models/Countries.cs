using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nata.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Accounts = new HashSet<Accounts>();
        }

        public int Id { get; set; }
        [Display(Name = "Country Name")]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}
