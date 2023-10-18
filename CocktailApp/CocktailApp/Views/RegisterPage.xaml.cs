using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace CocktailApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(UsernameEntry.Text) && !string.IsNullOrEmpty(EMailEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text) && !string.IsNullOrEmpty(PasswordRepeatEntry.Text))
            {
                HintFillAllFields.IsVisible = false;
                // Prozedere Bestätigungsmail
                OpenPopUp();
            }
            else
            {
                HintFillAllFields.IsVisible = true;
            }
        }

        private async void OpenPopUp()
        {
            var popup = new RegisterPopUpPage(); // Erstelle eine Instanz deiner Popup-Seite
            await PopupNavigation.Instance.PushAsync(popup); // Öffne das Popup-Fenster
        }
    }
}
