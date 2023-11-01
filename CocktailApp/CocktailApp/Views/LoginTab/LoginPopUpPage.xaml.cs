using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace CocktailApp.Views
{
    public partial class LoginPopUpPage : PopupPage
    {
        public LoginPopUpPage()
        {
            InitializeComponent();
        }

        private void OnCloseButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(); // Schließe das Popup-Fenster
            Navigation.PopAsync(); // Navigiere zur vorherigen Seite
        }
    }
}
