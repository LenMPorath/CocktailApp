using System;
using System.Collections.Generic;
using CocktailApp.ViewModels;
using CocktailApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CocktailApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RefreshTabsAsync();
        }

        public static void RefreshTabsAsync()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await SecureStorage.GetAsync("auth_token") != null)
                {
                    Current.CurrentItem.Items[1].IsVisible = true;
                    Current.CurrentItem.Items[2].IsVisible = true;
                    if (Convert.ToBoolean(await SecureStorage.GetAsync("isAdmin")))
                    {
                        Current.CurrentItem.Items[3].IsVisible = true;
                    }
                    else
                    {
                        Current.CurrentItem.Items[3].IsVisible = false;
                    }
                }
                else
                {
                    Current.CurrentItem.Items[1].IsVisible = false;
                    Current.CurrentItem.Items[2].IsVisible = false;
                    Current.CurrentItem.Items[3].IsVisible = false;
                }
        });
        }
    }
}
