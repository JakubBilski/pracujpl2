using System.Collections.Generic;

namespace MainApp.Models
{
	public class JobOfferDetailsHRView
	{
		public JobOffer Offer { get; set; }
		public HR HR { get; set; }
        public IEnumerable<Application> Applications { get; set; }
    }
}
