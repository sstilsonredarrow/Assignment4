using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Assignment4Core.Models;
using Assignment4Core.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;
using static Assignment4Core.Models.MapDirections;

namespace Assignment4Core.ViewModels
{
    public class MapViewModel : MvxViewModel
    {
       
        public Pin SelectedPin { get; set; }
        public PlacesOfInterest StartingPlace { get; set; }
        private ObservableCollection<PlacesOfInterest> _placesOfInterest;
        private IPlacesAPIService _placesService;
        public Position StartingPosition { get; set; }
        public List<string> SearchOptions { get; set; }
        public int SelectedSearchType { get; set; }

        public MapViewModel(IPlacesAPIService aPIService)
        {
            _placesService = aPIService;
            StartingPosition = new Position(44.5246085, -89.5852496);
            StartingPlace = new PlacesOfInterest();
            SearchOptions = new List<string>();
            SearchOptions.Add("restaurant");
            SearchOptions.Add("lodging");
            SearchOptions.Add("car_rental");
            SelectedSearchType = 0;          
        }

        public ObservableCollection<PlacesOfInterest> PlacesOfInterest
        {
            get { return _placesOfInterest; }
            set { _placesOfInterest = value; RaisePropertyChanged(nameof(PlacesOfInterest)); }
        }


        private MvxCommand<string> _searchCommand;
        public IMvxCommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new MvxCommand<string>(async (text) =>
                {
                    Console.WriteLine($"Search: {text}");
                    List<PlacesOfInterest> placesOfInterest = await _placesService.GetSearchResults(text, PlacesOfInterest[0].Position, SearchOptions[SelectedSearchType]);
                    if (PlacesOfInterest != null && PlacesOfInterest.Count > 0)
                    {
                        PlacesOfInterest.Clear();
                        placesOfInterest.ForEach(r => PlacesOfInterest.Add(r));

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
            _placesOfInterest = new ObservableCollection<PlacesOfInterest>();
            StartingPlace = new PlacesOfInterest();
            StartingPlace.Position = new Position(44.5246085, -89.5852496);
            StartingPlace.Address = "1001 Center Point Dr, Stevens Point, WI 54481";
            StartingPlace.PlaceName = "Midstate Technical College";
            _placesOfInterest.Add(StartingPlace);
            await RaisePropertyChanged(nameof(PlacesOfInterest));
        }    

    }
}
