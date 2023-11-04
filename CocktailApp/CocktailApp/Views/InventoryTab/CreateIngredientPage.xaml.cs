using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailApp.Views.InventoryTab
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateIngredientPage : ContentPage
	{
		public CreateIngredientPage()
		{
			InitializeComponent ();
		}

        private async void OnSaveClicked(object sender, EventArgs e)
        {

		}
	}
}