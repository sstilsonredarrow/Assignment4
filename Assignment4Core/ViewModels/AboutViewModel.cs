using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Assignment4Core.Models;
using Assignment4Core.Services;
using Assignment4Core.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Assignment4.ViewModels
{
    public class AboutViewModel : MvxViewModel
    {
        public string Title { get; set; }
        public AboutViewModel(IPlaceGetter placeGetter, IMvxNavigationService navigationService)
        {
            Title = "About";
            _placeGetter = placeGetter;
            _navigationService = navigationService;

            //OpenWebCommand = new MvxCommand(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ObservableCollection<PlacesSearching> Places { get; set; }
        private IPlaceGetter _placeGetter;
        private MvxCommand<PlacesSearching> _itemTappedCommand;
        private PlacesSearching _selectedItem;
        private IMvxNavigationService _navigationService;


        public IMvxCommand SelectCommand
        {
            get
            {
                return _itemTappedCommand = _itemTappedCommand ??
                        new MvxCommand<PlacesSearching>(async item =>
                        {
                            await _navigationService.Navigate<SearchViewModel, PlacesSearching>(item);
                        });
            }
        }

        public PlacesSearching SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                if (_selectedItem == null)
                    return;
                SelectCommand.Execute(_selectedItem);
                SelectedItem = null;
            }
        }

        public override Task Initialize()
        {
            base.Initialize();

            Places = _placeGetter.GetPlaces().Result;

            return Task.Delay(0);
        }
    }
}
