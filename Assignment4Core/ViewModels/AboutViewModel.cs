using System;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Assignment4.ViewModels
{
    public class AboutViewModel : MvxViewModel
    {
        public string Title { get; set; }
        public AboutViewModel()
        {
            Title = "About";

            //OpenWebCommand = new MvxCommand(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public IMvxCommand OpenWebCommand { get; }
    }
}