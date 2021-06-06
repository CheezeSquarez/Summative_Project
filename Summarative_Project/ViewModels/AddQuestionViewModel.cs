﻿using System.ComponentModel;
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
using System.Threading;


namespace Summarative_Project.ViewModels
{
    class AddQuestionViewModel : INotifyPropertyChanged
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
        string qText;
        string correctAnswer;
        string other1;
        string other2;
        string other3;
        string message;
        bool isEnabled;
        TriviaWebAPIProxy proxy;

        #endregion

        #region Properties
        public string QText
        {
            get => qText;
            set
            {
                qText = value;
                OnPropertyChanged();
            }
        }
        public string CorrectAnswer
        {
            get => correctAnswer;
            set
            {
                correctAnswer = value;
                OnPropertyChanged();
            }
        }
        public string Other1
        {
            get => other1;
            set
            {
                other1 = value;
                OnPropertyChanged();
            }
        }
        public string Other2
        {
            get => other2;
            set
            {
                other2 = value;
                OnPropertyChanged();
            }
        }
        public string Other3
        {
            get => other3;
            set
            {
                other3 = value;
                OnPropertyChanged();
            }
        }
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }
        public event Action Pop;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public ICommand AddCommand => new Command(Add);
        private async void Add()
        {
            IsEnabled = false;
            proxy = TriviaWebAPIProxy.CreateProxy();
            string nickname = ((App)App.Current).User.NickName;
            if (Other1 != "" && Other2 != "" && Other3 != "" && CorrectAnswer != "" && QText != "" && nickname != default(string))
            {
                string[] otherAnswers = { Other1, Other2, Other3 };
                AmericanQuestion q = new AmericanQuestion() { CorrectAnswer = this.CorrectAnswer, CreatorNickName = nickname, QText = this.QText, OtherAnswers = otherAnswers };
                bool status = await proxy.PostNewQuestion(q);
                if (status)
                {
                    Message = "Question Added Succesfully";
                    ((App)App.Current).User.Questions.Add(q);
                }
                    
                else
                    Message = "There was a problem adding you question. Try again later.";

            }

            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                // do something every 60 seconds
                Device.BeginInvokeOnMainThread(() =>
                {
                    Pop();
                });
                return false; // runs again, or false to stop
            });
           
        }
        public AddQuestionViewModel()
        {
            Other1 = "";
            Other2 = "";
            Other3 = "";
            CorrectAnswer = "";
            QText = "";
            message = "";

            isEnabled = true;
        }


    }
}
