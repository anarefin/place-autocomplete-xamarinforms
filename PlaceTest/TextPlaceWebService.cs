using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace PlaceTest
{
	// http://bertt.wordpress.com/2013/03/19/using-geonames-webservices-from-portable-class-library-pcl/

	public class TextPlaceWebService
	{
		const string MapsApiKey = "AIzaSyDd5QRpZKrk9UuvEYU-N1ZVcQDr5MzXjxk";

		public TextPlaceWebService ()
		{
		}

		public async Task<List<PredicPlaces>> GetLocationAsync (string search) {

			var client = new System.Net.Http.HttpClient ();

			client.BaseAddress = new Uri ("https://maps.googleapis.com/maps/api/place/autocomplete/");
			string afterBaseUrl = "json?input=" + search + "&" + "types=establishment" + "&" + "location=" + "37.76999,-122.44696" + "&" + "radius=500" +
			                      "&" + "key=" + MapsApiKey;

			var response = await client.GetAsync (afterBaseUrl);
			//var response = await client.GetAsync("json?input=Amoeba&types=establishment&location=37.76999,-122.44696&radius=500&key="+MapsApiKey);

			var placesJson = response.Content.ReadAsStringAsync().Result;

			/*
			dynamic rootObject = JsonConvert.DeserializeObject(placesJson);
			var predictions = rootObject.predictions;
			foreach (var place in predictions) {
				string des = place.description;
			} */

			JToken token = JObject.Parse(placesJson);

			string status = (string)token.SelectToken("status");
			var predictions = token.SelectToken("predictions");
			List<PredicPlaces> ppList = new List<PredicPlaces> ();
			ppList.Clear ();
			if (status == "OK") {

				foreach (var place in predictions) {
					PredicPlaces pp = new PredicPlaces ();
					pp.description = (string)place ["description"];
					pp.id = (string)place ["id"];
					ppList.Add (pp);
				}
			} 

			return ppList;
		}


		public async Task<string> GetEarthquakesAsync () {

			var client = new System.Net.Http.HttpClient ();

			client.BaseAddress = new Uri("http://api.geonames.org/");

			var response = await client.GetAsync("earthquakesJSON?north=44.1&south=-9.9&east=-22.4&west=55.2&username=bertt");

			var earthquakesJson = response.Content.ReadAsStringAsync().Result;

			//var rootobject = JsonConvert.DeserializeObject<Rootobject>(earthquakesJson);

			return earthquakesJson;

		}
			
	}
}

