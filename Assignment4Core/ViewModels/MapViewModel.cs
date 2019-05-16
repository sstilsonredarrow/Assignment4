using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Assignment4Core.Models;
using MvvmCross.ViewModels;
using MvvmCross.Commands;
using Assignment4Core.Services;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Assignment4Core.ViewModels
{
    public class MapViewModel : MvxViewModel
    {
        public MapPosition startPosition { get; set; }
        public ObservableCollection<MapPosition> startPositions { get; set; }
        private ObservableCollection<MapPosition> _placesOfInterest;
        private MvxCommand<string> _searchCommand;

        private IPlacesAPIService _placesService;
        public List<string> SearchType { get; set; }
        public int SelectedType { get; set; }

        public MapViewModel(IPlacesAPIService aPIService)
        {
            _placesService = aPIService;
            startPosition = new MapPosition();
            SearchType = new List<string>();
            SearchType.Add("restaurant");
            SearchType.Add("lodging");
            SearchType.Add("car_rental");
            SearchType.Add("store");
            SearchType.Add("bank");
            SearchType.Add("cafe");
            SearchType.Add("airport");
            SearchType.Add("hospital");
            SearchType.Add("gym");
            SearchType.Add("school");
            SelectedType = 0;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            _placesOfInterest = new ObservableCollection<MapPosition>() { startPosition };
            _placesOfInterest.ForEach(s => PlacesOfInterest.Add(s));
        }

        public ObservableCollection<MapPosition> PlacesOfInterest
        {
            get { return _placesOfInterest; }
            set { _placesOfInterest = value; RaisePropertyChanged(nameof(PlacesOfInterest)); }
        }

        public IMvxCommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new MvxCommand<string>(async (text) =>
                {
                    Console.WriteLine($"Search: {text}");
                    List<MapPosition> places = await _placesService.GetSearchResults(text, startPosition.Position, SearchType[SelectedType]);
                    if (places != null && places.Count > 0)
                    {
                        PlacesOfInterest.Clear();
                        places.ForEach(r => PlacesOfInterest.Add(r));

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await RaisePropertyChanged();
                        });
                    }

                }));
            }
        }
    }
}