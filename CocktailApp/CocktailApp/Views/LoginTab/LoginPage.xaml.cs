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
                try
                {
                    AAuthRequestModel auth = await AuthAPI.GetAuthWithEMail(EMailEntry.Text);

                    if (auth != null)
                    {
                        string newPasswordHash = PasswordService.ComputeHash(PasswordEntry.Text, auth.Salt);

                        if (auth.Password == newPasswordHash)
                        {
                            OpenPopUpLoginSuccessfull();
                            InputIsWrong.IsVisible = false;
                        }
                        else
                        {
                            InputIsWrong.IsVisible = true; // Zeige eine Meldung an, dass das Passwort falsch ist
                        }
                    }
                    else
                    {
                        InputIsWrong.IsVisible = true; // Zeige eine Meldung an, dass der Benutzer nicht gefunden wurde
                    }
                }
                catch (HttpRequestException exception)
                {
                    Console.WriteLine($"Fehler bei der Anfrage: {exception.Message}");
                    // Weitere Maßnahmen zur Fehlerbehandlung hier einfügen
                }
            }
            else
            {
                HintFillAllLoginFields.IsVisible = true; // Zeige eine Meldung an, dass alle Felder ausgefüllt werden müssen
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

        private async void OpenPopUpLoginSuccessfull()
        {
            var popup = new LoginPopUpPage(); // Erstelle eine Instanz deiner Popup-Seite
            await PopupNavigation.Instance.PushAsync(popup); // Öffne das Popup-Fenster
        }
    }
}
