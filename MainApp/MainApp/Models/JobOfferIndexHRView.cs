using System.Collections.Generic;

namespace MainApp.Models
{
	public class JobOfferIndexHRView
	{
		public IEnumerable<JobOffer> Offers { get; set; }
		public HR HR { get; set; }
	}
}
