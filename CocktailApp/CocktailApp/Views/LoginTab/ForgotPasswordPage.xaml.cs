using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EMailEntry.Text))
            {
                HintFillAllEMailFields.IsVisible = false;
                // TODO Prozedere Login
            }
            else
            {
                HintFillAllEMailFields.IsVisible = true;
            }
        }
    }
}