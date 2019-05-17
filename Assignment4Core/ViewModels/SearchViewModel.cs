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
        public Pin SelectedPin { get; set; }
        public PlaceOfInterest CurrentLocation { get; set; }
        private ObservableCollection<PlaceOfInterest> _placesOfInterest;
        private IPlacesAPIService _placesService;
        public Position StartPosition { get; set; }
        public List<string> SearchFilters { get; set; }
        public int selectedSearch;
        private MvxCommand<string> _searchCommand;

        public SearchViewModel(IPlacesAPIService placesService)
        {
            _placesService = placesService;
            StartPosition = new Position(44.52512, -89.58554);
            SearchFilters = new List<string>();
            SearchFilters.Add("restaurant");
            SearchFilters.Add("car_rental");
            SearchFilters.Add("lodging");
            // Selected search by int idea from Teegan Kennedy.
            SelectedSearch = 0;
        }

        public ObservableCollection<PlaceOfInterest> PlacesOfInterest
        {
            get { return _placesOfInterest; }
            set { _placesOfInterest = value; RaisePropertyChanged(nameof(PlacesOfInterest)); }
        }

        public int SelectedSearch
        {
            get { return selectedSearch; }
            set { selectedSearch = value; RaisePropertyChanged(nameof(SelectedSearch)); }
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
                        List<PlaceOfInterest> interest = await _placesService.GetSearchResults(text, PlacesOfInterest[0].Position, SearchFilters[SelectedSearch]);

                        if (interest != null && interest.Count > 0)
                        {
                            PlacesOfInterest.Clear();
                            interest.ForEach(r => PlacesOfInterest.Add(r));

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

        public override async Task Initialize()
        {
            // Organization pattern idea from Teegan Kennedy.
            await base.Initialize();
            _placesOfInterest = new ObservableCollection<PlaceOfInterest>();
            CurrentLocation = new PlaceOfInterest();
            CurrentLocation.Position = new Position(44.52512, -89.58554);
            CurrentLocation.Address = "1001 Center Point Dr, Stevens Point, WI 54481";
            CurrentLocation.PlaceName = "Midstate Technical College";
            _placesOfInterest.Add(CurrentLocation);
            await RaisePropertyChanged(nameof(PlacesOfInterest));
        }
    }
}
