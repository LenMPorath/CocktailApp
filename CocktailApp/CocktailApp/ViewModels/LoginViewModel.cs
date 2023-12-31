﻿using System;
using System.Collections.Generic;
using System.Text;
using CocktailApp.Views;
using Xamarin.Forms;

namespace CocktailApp.ViewModels
{
    public class LoginViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
