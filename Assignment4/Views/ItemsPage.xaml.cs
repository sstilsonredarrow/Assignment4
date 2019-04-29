using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Assignment4.Views;

namespace Assignment4.Views
{
    public partial class ItemsPage
    {

        public ItemsPage()
        {
            InitializeComponent();

            //BindingContext = viewModel = new ItemsViewModel();
        }

       
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Items.Count == 0)
                //viewModel.LoadItemsCommand.Execute(null);
        }
    }
}