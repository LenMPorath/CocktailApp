using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocktailApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            

            if (!string.IsNullOrEmpty(UsernameEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text))
            {
                HintFillAllLoginFields.IsVisible = false;
                // TODO Prozedere Login
            }
            else
            {
                HintFillAllLoginFields.IsVisible = true;
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            UsernameEntry.Text = "";
            PasswordEntry.Text = "";
            await Navigation.PushAsync(new RegisterPage()); // Navigiere zur "Registrierung"-Seite
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            UsernameEntry.Text = "";
            PasswordEntry.Text = "";
            await Navigation.PushAsync(new ForgotPasswordPage()); // Navigiere zur "Passwort Vergessen"-Seite
        }
    }
}
