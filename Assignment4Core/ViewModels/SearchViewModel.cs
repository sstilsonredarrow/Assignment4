using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Assignment4Core.Models;
using Assignment4Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;

namespace Assignment4Core.ViewModels
{
    public class SearchViewModel : MvxViewModel
    {
        private ObservableCollection<PlaceOfInterest> _placesOfInterest;
        private ObservableCollection<PlacesSearching> _places;
        private IPlacesAPIService _placesService;
        public Pin SelectedPin { get; set; }
        private IMvxNavigationService _navigationService;
        private MvxCommand<string> _searchCommand;
        private MvxCommand _getDirectionsCommand;
        public PlacesSearching PlaceSearching { get; set; }
        public string Address => PlaceSearching.Address;
        public string Description => PlaceSearching.PlaceName;
        public string category;

        public SearchViewModel(IMvxNavigationService navigationService, IPlacesAPIService placesService)
        {
            _navigationService = navigationService;
            _placesService = placesService;
            Result result = new Result();
            result.geometry = new Geometry();
            result.geometry.location = new Location();
            result.geometry.location.lat = 44.52512;
            result.geometry.location.lng = -89.58554;
            result.name = "MSTC";
            result.vicinity = "1001 Center Point Drive, Stevens Point, WI 54481";
            PlaceSearching = new PlacesSearching(result, "school");
        }

        public SearchViewModel()
        {
        }

        public ObservableCollection<PlaceOfInterest> PlacesOfInterest
        {
            get { return _placesOfInterest; }
            set { _placesOfInterest = value; RaisePropertyChanged(nameof(PlacesOfInterest)); }
        }

        public ObservableCollection<PlacesSearching> PlacesSearching
        {
            get { return _places; }
            set { _places = value; RaisePropertyChanged(nameof(PlacesSearching)); }
        }

        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        public IMvxCommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new MvxCommand<string>(async (text) =>
                {
                    try
                    {
                        Console.WriteLine($"Search: {text}");
                        List<PlaceOfInterest> interest = await _placesService.GetSearchResults(text, PlacesSearching[0].Position, "restaurant");
                        List<PlaceOfInterest> maybe = await _placesService.GetSearchResults(text, PlacesSearching[0].Position, "car_rental");
                        List<PlaceOfInterest> possible = await _placesService.GetSearchResults(text, PlacesSearching[0].Position, "lodging");

                        if (interest != null && interest.Count > 0)
                        {
                            PlacesOfInterest.Clear();
                            interest.ForEach(r => PlacesOfInterest.Add(r));
                            PlacesSearching.ForEach(s => PlacesOfInterest.Add(s));

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await RaisePropertyChanged();
                            });
                        }

                        if (maybe != null && maybe.Count > 0)
                        {
                            PlacesOfInterest.Clear();
                            maybe.ForEach(r => PlacesOfInterest.Add(r));
                            PlacesSearching.ForEach(s => PlacesOfInterest.Add(s));

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await RaisePropertyChanged();
                            });
                        }

                        if (possible != null && possible.Count > 0)
                        {
                            PlacesOfInterest.Clear();
                            possible.ForEach(r => PlacesOfInterest.Add(r));
                            PlacesSearching.ForEach(s => PlacesOfInterest.Add(s));

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await RaisePropertyChanged();
                            });
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }));
            }
        }



        public IMvxCommand GetDirectionsCommand
        {
            get
            {
                return _getDirectionsCommand ?? (_getDirectionsCommand = new MvxCommand(async () =>
                {
                    if (SelectedPin == null || SelectedPin.Position == PlacesSearching[0].Position)
                    {
                        return;
                    }
                    var result = await _placesService.GetDirections(PlacesSearching[0].Position, SelectedPin.Position);
                    Console.WriteLine("results returned");
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await RaisePropertyChanged();
                        await _navigationService.Navigate<DirectionsViewModel, RootDirections>(result);
                    });
                }));
            }
        }

        //public override async void Prepare(PlacesSearching parameter)
        //{
        //    PlaceSearching = parameter;
        //    var position = await Helpers.LocationHelper.Geocode(PlaceSearching.Address);
        //    PlaceSearching.Position = position;
        //    PlacesSearching = new ObservableCollection<PlacesSearching>() { PlaceSearching };
        //    PlacesOfInterest = new ObservableCollection<PlaceOfInterest>();
        //    PlacesSearching.ForEach(s => PlacesOfInterest.Add(s));
        //}

        public override async Task Initialize()
        {
            await base.Initialize();

            //PlaceSearching = parameter;
            var position = await Helpers.LocationHelper.Geocode(PlaceSearching.Address);
            PlaceSearching.Position = position;
            PlacesSearching = new ObservableCollection<PlacesSearching>() { PlaceSearching };
            PlacesOfInterest = new ObservableCollection<PlaceOfInterest>();
            PlacesSearching.ForEach(s => PlacesOfInterest.Add(s));
        }
    }
}
