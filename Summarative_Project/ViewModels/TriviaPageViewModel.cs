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
    class TriviaPageViewModel : INotifyPropertyChanged
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
        int streak;
        TriviaWebAPIProxy proxy;
        int score;
        string qText;
        string submittor;
        string correct;
        string[] mixed;
        bool canAddQuestion;
        bool canSkip;
        bool isLoggedIn;
        string color;
        #endregion

        #region Properties
        public string Submittor
        {
            get => submittor;
            private set
            {
                submittor = value;
                OnPropertyChanged();
            }
        }
        public string QText
        {
            get => qText;
            private set
            {
                qText = value;
                OnPropertyChanged();
            }
        }
        public int Streak
        {
            get => streak;
            set
            {
                streak = value;
                OnPropertyChanged();
            }
        }
        public int Score
        {
            get => score;
            set
            {
                score = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> Answers { get; set; }
        public string CorrectAnswer
        {
            get => correct;
            private set
            {
                correct = value;
                OnPropertyChanged();
            }
        }
        public bool CanAddQuestion
        {
            get => canAddQuestion;
            set
            {
                canAddQuestion = value;
                OnPropertyChanged();
            }
        }
        public bool CanSkip
        {
            get => canSkip;
            set
            {
                canSkip = value;
                OnPropertyChanged();
            }
        }
        public string Color
        {
            get => color;
            set
            {
                color = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ICommand SkipCommand => new Command(SkipMethod);
        public ICommand CheckAnswer => new Command<string>(CheckMethod);

        private void CheckMethod(string input)
        {
            if (input == this.CorrectAnswer)
            {
                QText = "CORRECT!";
                Color = "Green";
                Streak++;
                Score++;
                if (Streak >= 1)
                    CanSkip = true;
                if(Streak % 3 == 0 && isLoggedIn)
                    CanAddQuestion = true;
            }
            else
            {
                QText = "INCORRECT!";
                Color = "Red";
                Streak = 0;
                CanAddQuestion = false;
            }

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                // do something every 1 seconds
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShuffleArr();
                    Color = "#59abe3";
                });
                return false; // runs again, or false to stop
            });
            
        }

        private void SkipMethod()
        {
            if (CanSkip)
            {
                Streak--;
                Score--;
                if (Streak == 0)
                    CanSkip = false;
                ShuffleArr();
            }
        }
        private async void RefreshQuestions()
        {
            AmericanQuestion q = await proxy.GetRandomQuestion();
            Answers.Clear();
            this.QText = q.QText;
            this.CorrectAnswer = q.CorrectAnswer;
            this.Submittor = q.CreatorNickName;
            mixed[0] = q.CorrectAnswer;
            for (int i = 1; i < mixed.Length; i++)
            {
                mixed[i] = q.OtherAnswers[i - 1];
            }
            string[] temp = mixed;
            Random rnd = new Random();
            mixed = temp.OrderBy(x => rnd.Next()).ToArray();
            foreach (string s in mixed)
            {
                Answers.Add(s);
            }

        }
        private void ShuffleArr()
        {
            RefreshQuestions();
        }

        public TriviaPageViewModel()
        {
            CanSkip = false;
            CanAddQuestion = false;
            Answers = new ObservableCollection<string>();
            Streak = 0;
            Score = 0;
            proxy = TriviaWebAPIProxy.CreateProxy();
            mixed = new string[4];
            User u = ((App)App.Current).User;
            if (u != null)
                isLoggedIn = true;
            else
                isLoggedIn = false;
            Color = "#59abe3";
            ShuffleArr();
        }

    }
}
