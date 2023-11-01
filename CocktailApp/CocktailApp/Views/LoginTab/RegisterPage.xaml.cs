using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using CocktailApp.BackendAPI;
using CocktailApp.Services;

namespace CocktailApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(UsernameEntry.Text) && !string.IsNullOrEmpty(EMailEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text) && !string.IsNullOrEmpty(PasswordRepeatEntry.Text))
            {
                if (PasswordEntry.Text == PasswordRepeatEntry.Text)
                {
                    HintFillAllFields.IsVisible = false;
                    HintPasswordsDontMatch.IsVisible = false;
                    string salt = PasswordService.CreateSalt();
                    string passwordHash = PasswordService.ComputeHash(PasswordEntry.Text, salt);
                    await AuthAPI.CreateAuth(UsernameEntry.Text, passwordHash, salt, EMailEntry.Text);
                    OpenPopUp();
                }
                else
                {
                    HintFillAllFields.IsVisible = false;
                    HintPasswordsDontMatch.IsVisible = true;
                }
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
