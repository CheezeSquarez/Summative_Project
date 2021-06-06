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
    public partial class TriviaPage : ContentPage
    {
        TriviaPageViewModel context;
        public TriviaPage()
        {
            context = new TriviaPageViewModel();
            this.BindingContext = context;
            InitializeComponent();
        }

        private void AddQuestion_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddQuestion());
            context.CanAddQuestion = false;
        }
    }
}