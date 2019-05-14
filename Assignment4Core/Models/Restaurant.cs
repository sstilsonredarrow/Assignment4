using System;
namespace Assignment4Core.Models
{
    public class Restaurant : PlacesOfInterest
    {
        public string RestaurantName => PlaceName;

        public Restaurant(Result result)
        {
            PlaceName = result.name;
            Position = new Xamarin.Forms.Maps.Position(result.geometry.location.lat, result.geometry.location.lng);
            IconUrl = result.icon;
            Address = result.vicinity;
        }
    }
}
