using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Summarative_Project.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Summarative_Project.Models;

namespace Summarative_Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditQuestion : ContentPage
    {
        public EditQuestion(AmericanQuestion q)
        {
            EditQuestionViewModel context = new EditQuestionViewModel(q);
            context.Pop += () => Navigation.PopAsync();
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}