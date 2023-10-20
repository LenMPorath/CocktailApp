using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocktailApp.Views.RecipesTab;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipePage : ContentPage
    {
        public RecipePage()
        {
            InitializeComponent();
        }
        private async void OnCreateRecipeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateRecipePage()); // Navigiere zur "Rezept Erstellen"-Seite
        }
        

    }
}