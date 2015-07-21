using System;

namespace PlaceTest
{
	public class PredicPlaces
	{
		public string id { get; set; }
		public string description { get; set; }
		public string place_id { get; set; }
	  
	}

	public class Rootobject
	{
		public PredicPlaces[] places { get; set; }
	}
}

