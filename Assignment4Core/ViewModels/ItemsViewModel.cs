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
    public class ItemsViewModel : MvxViewModel
    {
        public PlaceOfInterest startPosition { get; set; }
        public ObservableCollection<PlaceOfInterest> startPositions { get; set; }
        private ObservableCollection<PlaceOfInterest> _placesOfInterest;
        private IPlacesAPIService _placesService;
        public List<string> SearchOptions { get; set; }
        public int SelectedSearchType { get; set; }

        public ItemsViewModel(IPlacesAPIService aPIService)
        {
            _placesService = aPIService;
            startPosition = new PlaceOfInterest();
            SearchOptions = new List<string>();
            SearchOptions.Add("restaurant");
            SearchOptions.Add("lodging");
            SearchOptions.Add("car_rental");
            SearchOptions.Add("art_gallery");
            SearchOptions.Add("bank");
            SearchOptions.Add("movie_theater");
            SelectedSearchType = 0;
        }

        public ObservableCollection<PlaceOfInterest> PlacesOfInterest
        {
            get { return _placesOfInterest; }
            set { _placesOfInterest = value;  RaisePropertyChanged(nameof(PlacesOfInterest)); }
        }

        private MvxCommand<string> _searchCommand;
        public IMvxCommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new MvxCommand<string>(async (text) =>
                {
                    Console.WriteLine($"Search: {text}");
                    List<PlaceOfInterest> places = await _placesService.GetSearchResults(text, startPosition.Position, SearchOptions[SelectedSearchType]);
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




        public override async Task Initialize()
        {
            await base.Initialize();
            _placesOfInterest = new ObservableCollection<PlaceOfInterest>() { startPosition };
            _placesOfInterest.ForEach(s => PlacesOfInterest.Add(s));
            // startPosition = 
        }
    }
}