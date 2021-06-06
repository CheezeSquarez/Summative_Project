using System;
using System.Collections.Generic;
using System.Text;
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
    class SignUpViewModel : INotifyPropertyChanged
    {

        #region Attributes
        string username;
        string password;
        string email;
        string error;
        TriviaWebAPIProxy proxy;
        #endregion

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
        public event Action Pop;

        public string Error
        {
            get => error;
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

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public ICommand SignUpCommand => new Command(SignUpMethod);

        private async void SignUpMethod()
        {
            if(Username != "" && Password != "" && Email != "")
            {
                User u = new User() { Email = this.Email, Password = this.Password, NickName = this.Username };
                bool isSuccess = await proxy.RegisterUser(u);
                if (isSuccess)
                {
                    Pop();
                    Error = "";
                }
                else
                    Error = "Something went wrong. Please try again later...";
            }
            else
                Error = "Please enter all neccesary info.";
        }

        public SignUpViewModel()
        {
            error = "";
            username = "";
            password = "";
            email = "";
            proxy = TriviaWebAPIProxy.CreateProxy();
        }
    }
}
