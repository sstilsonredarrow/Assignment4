using System;
using System.Threading.Tasks;
using Assignment4Core.Models;
using Xamarin.Forms.Maps;

namespace Assignment4Core.Services
{
    public interface IPlacesAPIService
    {
        Task<RootDirections> GetDirections(Position start, Position end);
    }
}
