using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLAppConfig;
using Assignment4Core.Models;
using Xamarin.Forms.Maps;
using Assignment4.Models;
using Assignment4Core.Services;

namespace Assignment4.Services
{
    public class PlacesAPIService : IPlacesAPIService
    {
        public PlacesAPIService()
        {
        }

        public async Task<List<PlaceOfInterest>> GetSearchResults(string search, Position position, string searchType)
        {
            using (var client = new HttpClient())
            {
                string baseAddr = ConfigurationManager.AppSettings["LocationSearchBaseURL"];
                string type = $"&type={searchType}"; // ADD SWAPPING HERE
                string name = search;
                string key = ConfigurationManager.AppSettings["APIKey"];
                var fullAddr = $"{baseAddr}{position.Latitude},{position.Longitude}&radius=1500{type}&name={name}&key=AIzaSyAzY34IDolpPu9LlH5NPr9mrEbLk9QvzYA";

                var response = await client.GetAsync(fullAddr);
                string content = await response.Content.ReadAsStringAsync();

                SearchResults result = JsonConvert.DeserializeObject<SearchResults>(content);
                List<PlaceOfInterest> placeResults = new List<PlaceOfInterest>();

                result.results.ForEach(r => placeResults.Add(new PlaceOfInterest(r)));
                return placeResults;

            }
        }
    }
}
