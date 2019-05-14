using System;
namespace Assignment4Core.Models
{
    public class Car_Rental : PlacesOfInterest
    {
        public string CarRentalName => PlaceName;

        public Car_Rental(Result result)
        {
            PlaceName = result.name;
            Position = new Xamarin.Forms.Maps.Position(result.geometry.location.lat, result.geometry.location.lng);
            IconUrl = result.icon;
            Address = result.vicinity;
        }
    }
}
