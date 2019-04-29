using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Assignment4Core.Models;
using MvvmCross.ViewModels;
using MvvmCross.Commands;
using Assignment4Core.Services;
using System.Collections.Generic;

namespace Assignment4Core.ViewModels
{
    public class ItemsViewModel : MvxViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public IMvxCommand LoadItemsCommand { get; set; }
        IDataStore<Item> _dataStore;
        public string Text { get; set; }

        public ItemsViewModel(IDataStore<Item> dataStore)
        {
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new MvxCommand(async () => await ExecuteLoadItemsCommand());
            _dataStore = dataStore;

        }

        async Task ExecuteLoadItemsCommand()
        {

            try
            {
                Items.Clear();
                var items = await _dataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
               
            }
        }
        public override async Task Initialize()
        {
            await base.Initialize();
            await ExecuteLoadItemsCommand();
        }
    }
}