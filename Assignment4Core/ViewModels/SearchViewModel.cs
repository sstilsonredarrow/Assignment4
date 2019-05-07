using System;
using System.Collections.ObjectModel;
using Assignment4Core.Models;
using Assignment4Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms.Maps;

namespace Assignment4Core.ViewModels
{
    public class SearchViewModel : MvxViewModel
    {
        private ObservableCollection<PlaceOfInterest> _placesOfInterest;
        private IPlacesAPIService _placesService;
        public Pin SelectedPin { get; set; }
        private IMvxNavigationService _navigationService;

        public SearchViewModel()
        {
        }
    }
}
