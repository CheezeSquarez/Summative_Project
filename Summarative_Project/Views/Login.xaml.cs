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
    public partial class Login : ContentPage
    {
        public Login()
        {
            LoginViewModel context = new LoginViewModel();
            context.Push += (p) => Navigation.PushAsync(p);
            context.PopToRoot += () => Navigation.PopToRootAsync();
            this.BindingContext = context;
            InitializeComponent();
            
        }

        

    }
}