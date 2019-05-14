using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using Assignment4Core.Models;

namespace TabsCore.Services
{
    public interface IPlacesAPIService
    {
        Task<List<SearchLocation>> GetSearchResults(string search, string type, Position position);
    }
}
