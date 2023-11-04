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
    public partial class EditAccountPage : ContentPage
    {
        public EditAccountPage()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            EMailEntry.Text = await SecureStorage.GetAsync("email");
            UsernameEntry.Text = await SecureStorage.GetAsync("username");
        }

        private async void OnEditPasswordButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPasswordPage());
        }
        private void OnSaveButtonClicked(object sender, EventArgs e)
        {

        }
    }
}