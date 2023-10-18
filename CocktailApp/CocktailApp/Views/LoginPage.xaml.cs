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

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            UsernameEntry.Text = "";
            PasswordEntry.Text = "";
            await Navigation.PushAsync(new RegisterPage()); // Navigiere zur Registrierungsseite
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            UsernameEntry.Text = "";
            PasswordEntry.Text = "";
            await Navigation.PushAsync(new ForgotPasswordPage()); // Navigiere zur Passwort Vergessen-Seite
        }
    }
}
