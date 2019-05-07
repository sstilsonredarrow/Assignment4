using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Assignment4Core.Models;
using Newtonsoft.Json;
using PCLAppConfig;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;

namespace Assignment4Core.Services
{
    public class PlacesAPIService : IPlacesAPIService
    {
        public PlacesAPIService()
        {
        }

        public async Task<RootDirections> GetDirections(Position start, Position end)
        {
            using (var client = new HttpClient())
            {
                string baseAddr = ConfigurationManager.AppSettings["GoogleMapsURL"];
                string key = ConfigurationManager.AppSettings["APIKey"];
                var fullAddr = $"{baseAddr}origin={start.Latitude},{start.Longitude}&destination={end.Latitude},{end.Longitude}&key={key}";

                var response = await client.GetAsync(fullAddr);
                string content = await response.Content.ReadAsStringAsync();
                RootDirections result = JsonConvert.DeserializeObject<RootDirections>(content);
                return result;
            }
        }

        public async Task<List<Restaurant>> GetSearchResults(string search, Position position)
        {
            using (var client = new HttpClient())
            {
                string baseAddr = ConfigurationManager.AppSettings["LocationSearchBaseURL"];
                string type = "&type=restaurant";
                string name = search;
                string key = ConfigurationManager.AppSettings["APIKey"];
                var fullAddr = $"{baseAddr}{position.Latitude},{position.Longitude}&radius=1500{type}&name={name}&key={key}";

                var response = await client.GetAsync(fullAddr);
                string content = await response.Content.ReadAsStringAsync();

                SearchResults result = JsonConvert.DeserializeObject<SearchResults>(content);
                List<Restaurant> restaurants = new List<Restaurant>();

                result.results.ForEach(r => restaurants.Add(new Restaurant(r)));

                return restaurants;
            }
        }
    }
}
