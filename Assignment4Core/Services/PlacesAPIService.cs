using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PCLAppConfig;
using Assignment4Core.Models;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;
using static Assignment4Core.Models.MapDirections;

namespace Assignment4Core.Services
{
    public class PlacesAPIService : IPlacesAPIService
    {
        public PlacesAPIService()
        {
        }
      

        public async Task<List<PlacesOfInterest>> GetSearchResults(string search, Position position, string searchType)
        {
            using (var client = new HttpClient())
            {
                string baseAddr = ConfigurationManager.AppSettings["LocationSearchBaseURL"];
                string type = $"&type={searchType}";
                string name = search;
                string key = ConfigurationManager.AppSettings["APIKey"];
                var fullAddr = $"{baseAddr}{position.Latitude},{position.Longitude}&radius=1500{type}&name={name}&key=AIzaSyAzY34IDolpPu9LlH5NPr9mrEbLk9QvzYA";

                var response = await client.GetAsync(fullAddr);
                string content = await response.Content.ReadAsStringAsync();

                SearchResults result = JsonConvert.DeserializeObject<SearchResults>(content);
                List<PlacesOfInterest> placesOfInterest = new List<PlacesOfInterest>();

                result.results.ForEach(r => placesOfInterest.Add(new PlacesOfInterest(r)));
                return placesOfInterest;
               
            }
        }
    }
}
