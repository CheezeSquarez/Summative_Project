using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Summarative_Project.ViewModels;

namespace Summarative_Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            this.BindingContext = new HomePageViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (((App)App.Current).User != null)
            {
                LoginBtn.IsVisible = false;
                UserBtn.IsVisible = true;
                SignOutBtn.IsVisible = true;
            }
            else
            {
                SignOutBtn.IsVisible = false;
                LoginBtn.IsVisible = true;
                UserBtn.IsVisible = false;
            }
        }
        private void SignOut_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignOut());
        }

        private void UserInfo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserPage());
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }
        private void Trivia_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TriviaPage());
        }
    }
}