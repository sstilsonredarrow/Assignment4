using System;
using Xamarin.Forms.Maps;
namespace Assignment4Core.Models
{
    public class PlacesOfInterest
    {
        public string PlaceName { get; set; }
        public string IconUrl { get; set; }
        public string Address { get; set; }
        public Position Position { get; set; }
        public Position StartingPosition { get; set; }

        public PlacesOfInterest()
        {

        }

        public PlacesOfInterest(Result r)
        {
            PlaceName = r.name;
            Position = new Xamarin.Forms.Maps.Position(r.geometry.location.lat, r.geometry.location.lng);
            Address = r.vicinity;
        }
    }
}
