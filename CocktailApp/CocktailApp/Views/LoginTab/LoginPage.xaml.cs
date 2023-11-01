using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CocktailApp.BackendAPI;
using CocktailApp.Models;
using CocktailApp.Services;
using CocktailApp.ViewModels;
using Rg.Plugins.Popup.Services;
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

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            InputIsWrong.IsVisible = false;
            HintFillAllLoginFields.IsVisible = false;

            if (!string.IsNullOrEmpty(EMailEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text))
            {
                string salt = await AuthAPI.GetSaltWithEMail(EMailEntry.Text);

                if (salt != null)
                {
                    string newPasswordHash = PasswordService.ComputeHash(PasswordEntry.Text, salt);
                    bool passwordCorrect = await AuthAPI.VerifyPassword(EMailEntry.Text, newPasswordHash);

                    if (passwordCorrect)
                    {
                        OpenPopUpLoginSuccessfull();
                        InputIsWrong.IsVisible = false;
                    }
                    else
                    {
                        InputIsWrong.IsVisible = true; // Zeige eine Meldung an, dass das Passwort falsch ist
                    }
                } else
                {
                    InputIsWrong.IsVisible = true;
                }
                
            } else
            {
                HintFillAllLoginFields.IsVisible = true;
            }
        }




        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            EMailEntry.Text = "";
            PasswordEntry.Text = "";
            await Navigation.PushAsync(new RegisterPage()); // Navigiere zur "Registrierung"-Seite
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            EMailEntry.Text = "";
            PasswordEntry.Text = "";
            await Navigation.PushAsync(new ForgotPasswordPage()); // Navigiere zur "Passwort Vergessen"-Seite
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            EMailEntry.Text = "";
            PasswordEntry.Text = "";
            OpenPopUpLogoutSuccessfull();
        }

        private async void OpenPopUpLoginSuccessfull()
        {
            var loginpopup = new LoginPopUpPage();
            await PopupNavigation.Instance.PushAsync(loginpopup);
        }

        private async void OpenPopUpLogoutSuccessfull()
        {
            var logoutpopup = new LogoutPopUpPage();
            await PopupNavigation.Instance.PushAsync(logoutpopup);
        }
    }
}
