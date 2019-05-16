using System;
using Assignment4.Models;
using Xamarin.Forms.Maps;

namespace Assignment4Core.Models
{
    public class MapPosition
    {
        public string PlaceName { get; set; }
        public string IconUrl { get; set; }
        public string Address { get; set; }
        public Position Position { get; set; }

        public MapPosition()
        {
            Position = new Position(44.5246283, -89.5854983);
            Address = "1001 Center Point Dr, Stevens Point, WI 54481";
            PlaceName = "Mid-State Technical College";
        }

        public MapPosition(Result r)
        {
            PlaceName = r.name;
            Position = new Xamarin.Forms.Maps.Position(r.geometry.location.lat, r.geometry.location.lng);
            IconUrl = r.icon;
            Address = r.vicinity;
        }
    }
}
