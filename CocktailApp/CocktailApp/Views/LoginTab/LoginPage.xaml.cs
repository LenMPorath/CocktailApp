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
using Xamarin.Essentials;
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
            RefreshComponents();
            this.BindingContext = new LoginViewModel();
        }

        private async void RefreshComponents()
        {
            if (await SecureStorage.GetAsync("auth_token") != null)
            {
                ForgotPasswordButton.IsEnabled = false;
                EMailEntry.IsVisible = false;
                PasswordEntry.IsVisible = false;
                LoginButton.IsEnabled = false;
                TextWelcomeBack.IsVisible = true;
                TextNoAccount.IsVisible = false;
                RegisterButton.IsEnabled = false;
                LogoutButton.IsEnabled = true;
                AppShell.RefreshTabsAsync();
            } else
            {
                ForgotPasswordButton.IsEnabled = true;
                EMailEntry.IsVisible = true;
                PasswordEntry.IsVisible = true;
                LoginButton.IsEnabled = true;
                TextWelcomeBack.IsVisible = false;
                TextNoAccount.IsVisible = true;
                RegisterButton.IsEnabled = true;
                LogoutButton.IsEnabled = false;
                AppShell.RefreshTabsAsync();
            }
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
                    ResponseData returnedData = await AuthAPI.VerifyPassword(EMailEntry.Text, newPasswordHash);
                    string token = returnedData.Token;
                    bool isAdmin = returnedData.IsAdmin;

                    if (token != null)
                    {
                        OpenPopUpLoginSuccessfull();
                        await SecureStorage.SetAsync("auth_token", token);
                        await SecureStorage.SetAsync("isAdmin", isAdmin.ToString());
                        InputIsWrong.IsVisible = false;
                        EMailEntry.Text = "";
                        PasswordEntry.Text = "";
                        RefreshComponents();
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

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("auth_token");
            EMailEntry.Text = "";
            PasswordEntry.Text = "";
            RefreshComponents();
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
