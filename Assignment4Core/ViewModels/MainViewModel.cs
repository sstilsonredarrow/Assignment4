using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Assignment4Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public override async Task Initialize()
        {
            await base.Initialize();

        }

        public override async void ViewAppearing()
        {
            await ShowInitialViewModels();
            base.ViewAppearing();
        }

        private async Task ShowInitialViewModels()
        {
            var tasks = new List<Task>();
           // tasks.Add(_navigationService.Navigate<BlueViewModel>());
            tasks.Add(_navigationService.Navigate <MapViewModel>());
            await Task.WhenAll(tasks);
        }
    }
}
