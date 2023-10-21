using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocktailApp.Views.InventoryTab;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryPage : ContentPage
    {
        public InventoryPage()
        {
            InitializeComponent();
        }

        private async void OnAddIngredientClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateIngredientPage()); // Navigiere zur "Zutat erstellen"-Seite
        }
    }
}