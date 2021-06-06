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
    public partial class AddQuestion : ContentPage
    {
        public AddQuestion()
        {
            AddQuestionViewModel context = new AddQuestionViewModel();
            context.Pop += () => Navigation.PopAsync();
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}