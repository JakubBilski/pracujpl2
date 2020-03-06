using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApp.Models
{
    public class ApplicationCreateView : Application
    {
        public IEnumerable<JobOffer> JobOffers { get; set; }
    }
}
