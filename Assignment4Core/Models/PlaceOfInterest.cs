using System;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;

namespace Assignment4Core.Models
{
    public class PlaceOfInterest
    {
        public string PlaceName { get; set; }
        public string IconUrl { get; set; }
        public string Address { get; set; }
        public Position Position { get; set; }
        public string CategoryType { get; set; }
    }
}
