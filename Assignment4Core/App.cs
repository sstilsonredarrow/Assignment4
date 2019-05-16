using System;
using MvvmCross.ViewModels;
using MvvmCross;
using Assignment4Core.ViewModels;
using Assignment4Core.Services;
using Assignment4Core.Models;
using PCLAppConfig;
using System.Reflection;

namespace Assignment4Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            Mvx.IoCProvider.RegisterType<IDataStore<Item>, MockDataStore>();
            Mvx.IoCProvider.RegisterType<IPlacesAPIService, PlacesAPIService>();
            Mvx.IoCProvider.RegisterType<IPlaceGetter, PlaceGetter>();
            //Assembly assembly = typeof(App).Assembly;
            //ConfigurationManager.Initialise(assembly.GetManifestResourceStream("Assignmen4Core.app.config"));
            RegisterAppStart<MainViewModel>();
        }
    }
}
