using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace Assignment4Core.Helpers
{
    public class LocationHelper
    {
        public static async Task<Position> Geocode(string address)
        {
            try
            {
                var locations = await Geocoding.GetLocationsAsync(address);
                if (locations != null)
                {
                    var location = locations?.FirstOrDefault();
                    if (location != null)
                    {
                        Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
                        return new Position(location.Latitude, location.Longitude);
                    }
                }
                return new Position(0, 0);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine(fnsEx.StackTrace);
                return new Position(0, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new Position(0, 0);
            }
        }
    }
}
