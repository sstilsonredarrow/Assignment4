using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment4Core.Models;
using Xamarin.Forms.Maps;
using static Assignment4Core.Models.MapDirections;

namespace Assignment4Core.Services
{
    public interface IPlacesAPIService
    {
        Task<List<PlacesOfInterest>> GetSearchResults(string search, Position position, string searchType);
    }
}
