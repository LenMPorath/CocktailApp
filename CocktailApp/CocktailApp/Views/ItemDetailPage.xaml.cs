using System.ComponentModel;
using CocktailApp.ViewModels;
using Xamarin.Forms;

namespace CocktailApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}