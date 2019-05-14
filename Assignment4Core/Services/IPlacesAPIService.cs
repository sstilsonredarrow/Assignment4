using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment4Core.Models;
using Xamarin.Forms.Maps;

namespace Assignment4Core.Services
{
    public interface IPlacesAPIService
    {
        Task<List<PlaceOfInterest>> GetSearchResults(string search, Position position, string searchType);
    }
}
