using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nata.Models
{
    public class ActivitiesViewModel
    {
        public List<Activities> Activities { get; set; }
        public SelectList Accounts { get; set; }
        public SelectList Users { get; set; }
        public string SearchClient { get; set; }
        public string searchUserName { get; set; }
        public string SearchString { get; set; }
        public string UserName { get; set; }

    }
}
