using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace CocktailApp.Views
{
    public partial class LogoutPopUpPage : PopupPage
    {
        public LogoutPopUpPage()
        {
            InitializeComponent();
        }

        private void OnCloseButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
            Navigation.PopAsync();
        }
    }
}
