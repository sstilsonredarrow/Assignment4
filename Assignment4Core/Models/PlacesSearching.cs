using System;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;

namespace Assignment4Core.Models
{
    public class PlacesSearching : PlaceOfInterest
    {
        public string PlaceSearchingName => PlaceName;

        public PlacesSearching(Result result, string category)
        {
            PlaceName = result.name;
            Position = new Position(result.geometry.location.lat, result.geometry.location.lng);
            IconUrl = result.icon;
            Address = result.vicinity;
            CategoryType = category;
        }
    }

    public class PlacesList
    {
        public ObservableCollection<PlacesSearching> places { get; set; }
    }
}
