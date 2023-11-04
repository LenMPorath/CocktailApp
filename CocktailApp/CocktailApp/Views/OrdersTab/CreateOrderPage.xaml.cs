using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailApp.Views.OrdersTab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateOrderPage : ContentPage
    {

        public class Recipe
        {
            public string RecipeName { get; set; }
            public float Kcal { get; set; }
            public bool IsAvailable { get; set; }
        }
        public CreateOrderPage()
        {
            InitializeComponent();
            LoadUsername();

        }

        private async void LoadUsername()
        {
            string username = await SecureStorage.GetAsync("username");
            CreatedByEntry.Text = username;
        }
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(e.NewTextValue, out _))
            {
                var entry = sender as Entry;
                if (entry != null && e.NewTextValue.Length > 0)
                {
                    entry.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                }
            }
        }

        private void OnCreateOrderClicked(object sender, EventArgs e)
        {
            AmountEntry.Text = "2";
        }

    }
}