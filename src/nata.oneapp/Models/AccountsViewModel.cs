using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace nata.Models
{
    public class AccountsViewModel
    {
        public List<Accounts> Accounts { get; set; }
        public SelectList Status  { get; set; }
        public string SearchStatus { get; set; }
        public string SearchString { get; set; }

    }
}
