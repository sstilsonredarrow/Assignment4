using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Assignment4Core.Models;
using Newtonsoft.Json;
using PCLAppConfig;
using Xamarin.Forms.Maps;

namespace TabsCore.Services
{
    public class PlacesAPIService : IPlacesAPIService
    {
        public PlacesAPIService()
        {
        }

        public async Task<List<SearchLocation>> GetSearchResults(string search, string type, Position position)
        {
            using (var client = new HttpClient())
            {
                string baseAddr = ConfigurationManager.AppSettings["LocationSearchBaseURL"];
                string locationType = $"&type={type}";
                string name = search;
                string key = ConfigurationManager.AppSettings["APIKey"];
                var fullAddr = $"{baseAddr}{position.Latitude},{position.Longitude}&radius=1500{locationType}&name={name}&key={key}";

                var response = await client.GetAsync(fullAddr);
                string content = await response.Content.ReadAsStringAsync();

                SearchResults result = JsonConvert.DeserializeObject<SearchResults>(content);
                List<SearchLocation> restaurants = new List<SearchLocation>();

                result.results.ForEach(r => restaurants.Add(new SearchLocation(r)));

                return restaurants;
            }
        }
    }
}
