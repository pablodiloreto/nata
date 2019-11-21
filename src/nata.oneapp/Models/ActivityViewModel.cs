using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nata.Models
{
    public class ActivityViewModel
    {
        public Activities Activity { get; set; }
        public SelectList UsersAssignedTo { get; set; }
        public string AssignedToUsername { get; set; }
        public string AccountId { get; set; }

    }
}
