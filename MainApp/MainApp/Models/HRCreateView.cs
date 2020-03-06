using System.Collections.Generic;

namespace MainApp.Models
{
	public class HRCreateView : HR 
	{
		public IEnumerable<Company> Companies { get; set; }
	}
}
