using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Assignment4Core.Models;

namespace Assignment4Core.Services
{
    public interface IPlaceGetter
    {
        Task<ObservableCollection<PlacesSearching>> GetPlaces();
    }
}
