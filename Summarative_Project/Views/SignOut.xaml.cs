using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Summarative_Project.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Summarative_Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignOut : ContentPage
    {
        public SignOut()
        {
            InitializeComponent();
        }

        private void Yes_Clicked(object sender, EventArgs e)
        {
            ((App)App.Current).User = null;
            Navigation.PopToRootAsync();
        }

        private void No_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}