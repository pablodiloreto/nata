using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace nata.Models
{
    public class ContractsViewModel
    {
        public List<Contracts> Contracts { get; set; }
        public SelectList Accounts { get; set; }
        public SelectList Status { get; set; }
        public string SearchStatus { get; set; }
        public string SearchClient { get; set; }
        public string SearchString { get; set; }

    }
}
