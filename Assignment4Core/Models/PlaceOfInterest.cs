using System;
using Assignment4.Models;
using Xamarin.Forms.Maps;

namespace Assignment4Core.Models
{
    public class PlaceOfInterest
    {
        public string PlaceName { get; set; }
        public string IconUrl { get; set; }
        public string Address { get; set; }
        public Position Position { get; set; }

        public PlaceOfInterest(Result r)
        {
            PlaceName = r.name;
            Position = new Xamarin.Forms.Maps.Position(r.geometry.location.lat, r.geometry.location.lng);
            IconUrl = r.icon;
            Address = r.vicinity;
        }

        public PlaceOfInterest()
        {
            Position = new Position(44.5246283, -89.5854983);
            Address = "Midstate Technical College";
            PlaceName = "You Are Here";
        }
    }
}
