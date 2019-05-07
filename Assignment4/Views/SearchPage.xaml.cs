using System;
using System.Collections.Generic;
using Assignment4Core.ViewModels;
using MvvmCross.Binding;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Assignment4.Views
{
    public partial class SearchPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Console.WriteLine("In the handle clicked");
            (this.BindingContext.DataContext as SearchViewModel()).SelectedPin = sender as Pin;
        }

        void Handle_Steps(object sender, System.EventArgs e)
        {
            var pin = (this.BindingContext.DataContext as SearchViewModel).SelectedPin;
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
            var position = (this.BindingContext.DataContext as MapViewModel).School.Position;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(3)).WithZoom(10));
        }
    }
}
