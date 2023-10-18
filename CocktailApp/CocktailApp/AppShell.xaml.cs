﻿using System;
using System.Collections.Generic;
using CocktailApp.ViewModels;
using CocktailApp.Views;
using Xamarin.Forms;

namespace CocktailApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}