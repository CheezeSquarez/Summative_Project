using System.ComponentModel;
using System.Windows.Input;
using Summarative_Project.Services;
using Xamarin.Forms;
using Summarative_Project.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Text;
using System.Collections.ObjectModel;
using Summarative_Project.Views;

namespace Summarative_Project.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {

        #region Attributes
        //Attributes

        private string password;
        TriviaWebAPIProxy proxy;
        private string username;
        private string error;
        #endregion

        //Contructor
        public LoginViewModel()
        {
            error = "";
            username = "";
            password = "";
            proxy = TriviaWebAPIProxy.CreateProxy();
        }

        #region INotifyPropertyChanged Implementation
        //INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        //OnPropertyChanged Event
        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        //Properties
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public string Error
        {
            get => this.error;
            set
            {
                error = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        public event Action<Page> Push;
        public event Action PopToRoot;
        #endregion

        //Commands
        public ICommand Login => new Command(LoginMethod);
        public ICommand TapCommand => new Command(TapMethod);

        //Methods

        private void TapMethod()
        {
            Error = "";
            Push?.Invoke(new SignUp());
        }
        
        private async void LoginMethod()
        {
             
            User user = await proxy.LoginAsync(Username, Password);
            if (user == null)
                this.Error = "Invalid username or password. Please try again...";
            else
            {
                Error = "";
                ((App)App.Current).User = user;
                PopToRoot?.Invoke();
            }
                
        }

        
    }
}