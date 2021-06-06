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
    public partial class UserPage : ContentPage
    {
        private UserPageViewModel context;
        public UserPage()
        {
            context = new UserPageViewModel();
            context.Push += (p) => Navigation.PushAsync(p);
            this.BindingContext = context;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            context = new UserPageViewModel();
        }
    }
}