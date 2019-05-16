using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment4Core.Models;
using Xamarin.Forms.Maps;

namespace Assignment4Core.Services
{
    public interface IPlacesAPIService
    {
        Task<List<MapPosition>> GetSearchResults(string search, Position position, string searchType);
    }
}
