using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Api
{
	public class AppSettings
	{
		public string Secret { get; set; }
		public int Exp { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
	}
}
