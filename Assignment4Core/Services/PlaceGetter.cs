using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Assignment4Core.Models;
using Newtonsoft.Json;

namespace Assignment4Core.Services
{
    public class PlaceGetter : IPlaceGetter
    {
        public PlaceGetter()
        {
        }

        public Task<ObservableCollection<PlacesSearching>> GetPlaces()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(PlaceGetter)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("Assignment4Core.places.json");
            string text = string.Empty;
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            PlacesList placesList = JsonConvert.DeserializeObject<PlacesList>(text);

            return Task.FromResult(placesList.places);
        }
    }
}
