using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

using Xamarin.Forms;
using Assignment4Core.ViewModels;

namespace Assignment4.Views
{
    public partial class MapPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Console.WriteLine("In the handle clicked!");
            (this.BindingContext.DataContext as MapViewModel).SelectedPin = sender as Pin;
        }

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            (this.BindingContext.DataContext as MapViewModel).SelectedType = (sender as Picker).SelectedItem.ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var position = (this.BindingContext.DataContext as MapViewModel).CurrentLocation.Position;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(2)).WithZoom(10));
        }
    }
}
