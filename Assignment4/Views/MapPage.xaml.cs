using System;
using System.Collections.Generic;
using System.Net;
using Assignment4Core.ViewModels;
using Assignment4Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

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
            Console.WriteLine("In the handle_clicked.");
            (this.BindingContext.DataContext as MapViewModel).SelectedPin = sender as Pin;
        }

        void Handle_Steps(object sender, System.EventArgs e)
        {
            var pin = (this.BindingContext.DataContext as MapViewModel).SelectedPin;
            if (pin == null)
                return;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Device.OpenUri(new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(pin.Address))));
                    break;
                case Device.Android:
                    Device.OpenUri(new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(pin.Address))));
                    break;
                default:
                    break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var position = (this.BindingContext.DataContext as MapViewModel).StartingPosition; 
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(2)).WithZoom(10));
        }
    }
}
