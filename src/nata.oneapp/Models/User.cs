using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace nata.Models
{
    public class User
    {
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Email Address")]
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

    }
}
