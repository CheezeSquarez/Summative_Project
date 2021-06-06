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
using System.Collections.Generic;

namespace Summarative_Project.ViewModels
{
    class UserPageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        //INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        //OnPropertyChanged Event
        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Attributes
        User u;
        string name;
        string email;
        string password;
        public ObservableCollection<AmericanQuestion> Questions { get; set; }
        TriviaWebAPIProxy proxy;
        #endregion

        #region Properties
        public string Name
        {
            get => this.name;
            set
            {
                name = value;
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

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public event Action<Page> Push;

        public ICommand EditCommand => new Command<AmericanQuestion>(Edit);
        private void Edit(AmericanQuestion q)
        {
            Push.Invoke(new EditQuestion(q));
        }

        public ICommand DeleteCommand => new Command<AmericanQuestion>(Delete);
        private async void Delete(AmericanQuestion q)
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            bool worked = await proxy.DeleteQuestion(q);
            if (worked)
            {
                ((App)App.Current).User.Questions.Remove(q);
                this.Questions.Remove(q);
            }
                
        }
        public UserPageViewModel()
        {
            this.Email = ((App)App.Current).User.Email;
            this.Name = ((App)App.Current).User.NickName;
            this.password = ((App)App.Current).User.Password;
            this.Questions = new ObservableCollection<AmericanQuestion>(((App)App.Current).User.Questions);
            this.proxy = TriviaWebAPIProxy.CreateProxy();
            
        }

    }
}
