using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Summarative_Project.Views;
using Summarative_Project.Models;



namespace Summarative_Project
{
    public partial class App : Application
    {
        public User User { get; set; }
        public App()
        {
            User = null;
            InitializeComponent();
            MainPage = new NavigationPage(new HomePage()) { BarBackgroundColor = Color.FromHex("#2e3131") };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
