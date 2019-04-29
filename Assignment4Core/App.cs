using System;
using MvvmCross.ViewModels;
using MvvmCross;
using Assignment4Core.ViewModels;
using Assignment4Core.Services;
using Assignment4Core.Models;

namespace Assignment4Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            Mvx.IoCProvider.RegisterType<IDataStore<Item>, MockDataStore>();
            RegisterAppStart<MainViewModel>();
        }
    }
}
