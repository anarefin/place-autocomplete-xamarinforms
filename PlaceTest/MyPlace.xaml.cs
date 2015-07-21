using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace PlaceTest
{
	public partial class MyPlace : ContentPage
	{
		

		public MyPlace ()
		{
			InitializeComponent ();

			lvPredicPlaces.RowHeight = 65;
			lvPredicPlaces.HasUnevenRows = true;
			//lvPredicPlaces.ItemTemplate = new DataTemplate(typeof(TextCell));
			//lvPredicPlaces.ItemTemplate.SetBinding(TextCell.TextProperty, "description");
			lvPredicPlaces.ItemSelected += (sender, e) => {
				var place = (PredicPlaces)e.SelectedItem;
				DisplayAlert("Place info",place.description, "OK");
			};
			//lvPredicPlaces.IsVisible = false;

		}

		// SearchBar search text change event
		void searchBarTextChanged (object sender, TextChangedEventArgs textChangedEventArgs)
		{
			// Has Backspace or Cancel has been pressed?
			if (textChangedEventArgs.NewTextValue == string.Empty)
			{
				// Cancel pressed
				if (textChangedEventArgs.OldTextValue.Length > 1){}
				else {}
							
			}
		}


		// SearchBar SearchButton press event
		async void searchBtnPressed (object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty (searchBar.Text)) {
				
				TextPlaceWebService ws = new TextPlaceWebService ();
				var result = await ws.GetLocationAsync (searchBar.Text);

				Xamarin.Forms.Device.BeginInvokeOnMainThread( () => {
					lvPredicPlaces.ItemsSource = result;

				});

				searchBar.Unfocus ();
			}
		}


		 
	}
}

