using System;
namespace Assignment4Core.Models
{
    public class Lodging : PlacesOfInterest
    {
        public string LodgingName => PlaceName;

        public Lodging(Result result)
        {
            PlaceName = result.name;
            Position = new Xamarin.Forms.Maps.Position(result.geometry.location.lat, result.geometry.location.lng);
            IconUrl = result.icon;
            Address = result.vicinity;
        }
    }
}
