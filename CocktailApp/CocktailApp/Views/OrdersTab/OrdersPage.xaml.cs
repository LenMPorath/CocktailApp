using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocktailApp.Views.OrdersTab;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        public OrdersPage()
        {
            InitializeComponent();
        }

        private async void OnCreateOrderClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateOrderPage()); // Navigiere zur "Auftrag Erstellen"-Seite
        }
    }
}