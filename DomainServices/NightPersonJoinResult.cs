using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF
{
	public class NightPersonJoinResult
	{
		public Night Night { get; set; }
		public Person Person { get; set; }
	}
}
