using System;
using Xamarin.Forms.Maps;
using Assignment4Core.ViewModels;

namespace Assignment4.Views
{
    public partial class ItemsPage
    {

        public ItemsPage()
        {
            InitializeComponent();

            //BindingContext = viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var position = (this.BindingContext.DataContext as ItemsViewModel).startPosition.Position;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(3)).WithZoom(10));

            //if (viewModel.Items.Count == 0)
            //viewModel.LoadItemsCommand.Execute(null);
        }
    }
}