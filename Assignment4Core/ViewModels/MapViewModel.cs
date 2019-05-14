using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assignment4Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TabsCore.Helpers;
using TabsCore.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;

namespace Assignment4Core.ViewModels
{
    public class MapViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;
        private ObservableCollection<SearchLocation> _searchLocations;
        private ObservableCollection<PlaceOfInterest> _placesOfInterest;
        private List<string> _searchType;
        private IPlacesAPIService _placesService;
        public Pin SelectedPin { get; set; }
        public string SelectedType { get; set; }

        public List<string> SearchType
        {
            get { return _searchType; }
            set { _searchType = value; RaisePropertyChanged(nameof(PlacesOfInterest)); }
        }

        public PlaceOfInterest CurrentLocation = new PlaceOfInterest
        {
            Address = "1600 Pennsylvania Ave NW, Washington, DC 20500",
            PlaceName = "The White House"
        };

        public ObservableCollection<SearchLocation> SearchLocations
        {
            get { return _searchLocations ?? new ObservableCollection<SearchLocation>(); }
            set { _searchLocations = value; RaisePropertyChanged(nameof(SearchLocations)); }
        }

        public ObservableCollection<PlaceOfInterest> PlacesOfInterest
        {
            get { return _placesOfInterest; }
            set { _placesOfInterest = value; RaisePropertyChanged(nameof(PlacesOfInterest)); }
        }

        public MapViewModel(IMvxNavigationService navigationService, IPlacesAPIService aPIService)
        {
            _navigationService = navigationService;
            _placesService = aPIService;
        }

        private MvxCommand<string> _searchCommand;
        public IMvxCommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new MvxCommand<string>(async (text) =>
                {
                Console.WriteLine($"Search: {text}");
                    List<SearchLocation> locations = await _placesService.GetSearchResults(text, SelectedType, CurrentLocation.Position);
                if (locations != null && locations.Count > 0)
                {
                    PlacesOfInterest.Clear();
                    locations.ForEach(r => PlacesOfInterest.Add(r));
                        PlacesOfInterest.Add(CurrentLocation);

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await RaisePropertyChanged();
                        });
                    }

                }));
            }
        }

        public async override void Prepare()
        {
            SearchType = new List<string>()
            {
                "restaurant",
                "lodging",
                "car_rental",
                "bar",
                "pharmacy"
            };
            SelectedType = SearchType[0];
            var position = await LocationHelper.Geocode(CurrentLocation.Address);
            CurrentLocation.Position = position;
            PlacesOfInterest = new ObservableCollection<PlaceOfInterest>();
            PlacesOfInterest.Add(CurrentLocation);
            await RaisePropertyChanged(nameof(SearchType));
        }
    }
}
