using System;
using Xamarin.Forms.Maps;
using Assignment4Core.ViewModels;
using Xamarin.Forms;

namespace Assignment4.Views
{
    public partial class MapPage
    {

        public MapPage()
        {
            InitializeComponent();

            //BindingContext = viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var position = (this.BindingContext.DataContext as MapViewModel).startPosition.Position;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(3)).WithZoom(10));
        }

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            (this.BindingContext.DataContext as MapViewModel).SelectedType = (int)(sender as Picker).SelectedIndex;
        }
    }
}