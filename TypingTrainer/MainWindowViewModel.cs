using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TypingTrainer.Utils;

namespace TypingTrainer
{
    class MainWindowViewModel : BindableBase
    {

        //public MainWindowViewModel()
        //{
        //    EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyUpEvent, new KeyEventHandler(KeyUp), true);
        //}

        //private void KeyUp(object sender, KeyEventArgs e)
        //{
        //    Console.WriteLine(e.Key);
        //    //e.SystemKey
        //}

        public MainWindowViewModel()
        {
            CurrentChallenge = "Write this if you can!";
        }

        public int TotalCorrect
        {
            get => Get<int>();
            set
            {
                Set(value);
                OnPropertyChanged(nameof(TotalAttempts));
            }
        }

        public int TotalFailed
        {
            get => Get<int>();
            set
            {
                Set(value);
                OnPropertyChanged(nameof(TotalAttempts));
            }
        }

        public int TotalAttempts => TotalCorrect + TotalFailed;

        public bool Correct
        {
            get => Get<bool>();
            set =>Set(value);
        }

        public string CurrentChallenge
        {
            get => Get<string>();
            set => Set(value);
        }

        public string UserInput
        {
            get => Get<string>();
            set
            {
                Set(value);
                CheckSolutionSoFar();
                //CheckSolution();
            }
        }


        /// <summary>
        /// Checks if the text is exactly right so far, if not reset!
        /// </summary>
        private void CheckSolutionSoFar()
        {
            var a = UserInput;
            var b = CurrentChallenge.Substring(0, UserInput.Length);
            if (a == b)
            {
                // ok, do nothing
            }
            else
            {
                UserInput = "";
                TotalFailed++;
            }

            if (UserInput == CurrentChallenge)
            {
                // we are done!
                Correct = true;
                TotalCorrect++;
                UserInput = "";
            }
        }

        private void CheckSolution()
        {
            Correct = IsCorrect(UserInput, CurrentChallenge);

            if (Correct)
            {
                // evolve the challenge
                CurrentChallenge += "1";
            }
        }

        /// <summary>
        /// Checks 
        /// </summary>
        private static bool IsCorrect(string a, string b)
        {
            try
            {
                return string.Equals(a, b);
            }
            catch (Exception e)
            {
                throw;
                //return false;
            }
        }
    }
}
