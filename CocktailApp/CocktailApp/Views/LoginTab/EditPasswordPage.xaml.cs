using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPasswordPage : ContentPage
    {
        public EditPasswordPage()
        {
            InitializeComponent();
        }
        private void OnSaveButtonClicked(object sender, EventArgs e)
        {

        }
    }
}