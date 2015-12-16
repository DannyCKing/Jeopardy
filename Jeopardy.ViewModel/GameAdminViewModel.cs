using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using Jeopardy.Model;
using Jeopardy.Model.Interfaces;
using Jeopardy.ViewModel.Commands;

namespace Jeopardy.ViewModel
{
    public class GameAdminViewModel : NotifyPropertyChanged
    {
        private Game _currentGame;

        public Game CurrentGame
        {
            get
            {
                return _currentGame;
            }
            set
            {
                _currentGame = value;
                OnPropertyChanged("CurrentGame");
            }
        }

        private bool _currentRoundStarted = false;

        public bool CurrentRoundStarted
        {
            get
            {
                return _currentRoundStarted;
            }
            set
            {
                _currentRoundStarted = value;
                OnPropertyChanged("CurrentRoundStarted");
            }
        }

        public ICommand StartGameCommand { get; private set; }

        public ICommand OnSelectQuestionCommand { get; private set; }

        public ICommand OnSelectPlayerCommand { get; private set; }

        public ICommand CorrectAnswerCommand { get; private set; }

        public ICommand IncorrectAnswerCommand { get; private set; }

        public ICommand OnNoAnswerCommand { get; private set; }

        public ICommand StartRoundCommand { get; private set; }

        public ICommand NextRoundCommand { get; private set; }

        public GameAdminViewModel()
        {
            StartGameCommand = new ActionCommand(StartGame);
            OnSelectQuestionCommand = new RelayCommand(SelectQuestion, CanSelectQuestion);
            CorrectAnswerCommand = new RelayCommand(OnCorrectAnswer, CanSelectResponse);
            IncorrectAnswerCommand = new RelayCommand(OnWrongAnswer, CanSelectResponse);
            OnSelectPlayerCommand = new RelayCommand(OnSelectPlayer, CanSelectPlayer);
            OnNoAnswerCommand = new RelayCommand(OnNoAnswer, CanSelectNoAnswer);
            StartRoundCommand = new RelayCommand(OnStartRound, CanStartRound);
            NextRoundCommand = new RelayCommand(OnStartNextRound, CanStartNextRound);
        }

        private void StartGame(object param = null)
        {
            foreach (var cat in CurrentGame.CurrentRound.Gameboard.Categories)
            {
                foreach (var quest in cat.Questions)
                {
                    quest.OnQuestionSelected = OnSelectQuestionCommand;
                }
            }

            foreach (var player in CurrentGame.Players)
            {
                player.OnSelectPlayerCommand = OnSelectPlayerCommand;
            }

        }

        private void SelectQuestion(object question = null)
        {
            Question selectedQuestion = question as Question;
            if (selectedQuestion != null)
            {
                CurrentGame.CurrentRound.Gameboard.CurrentFocus = selectedQuestion;
                CurrentGame.CurrentRound.Gameboard.CurrentFocus.IsPlayed = true;

                foreach (var player in CurrentGame.Players)
                {
                    player.HasAnswered = false;
                }
            }
            this.CurrentGame.PlayerAnswering = null;
            CommandManager.InvalidateRequerySuggested();

            Timer colorTimer = new Timer();
            colorTimer.Interval = 8000;
            colorTimer.Enabled = true;
            colorTimer.Tick += colorTimer_Tick;
        }

        void colorTimer_Tick(object sender, System.EventArgs e)
        {
            if (CurrentGame != null && CurrentGame.CurrentRound != null && CurrentGame.CurrentRound.Gameboard != null &&
                CurrentGame.CurrentRound.Gameboard.CurrentFocus != null)
            {
                CurrentGame.CurrentRound.Gameboard.CurrentFocus.BackgroundColor = Color.Green;
            }
        }



        private bool CanSelectQuestion(object question = null)
        {
            Question selectedQuestion = question as Question;
            if (selectedQuestion != null)
            {
                return !selectedQuestion.IsPlayed;
            }
            return false;
        }

        private void OnCorrectAnswer(object parameter = null)
        {
            if (this.CurrentGame.PlayerAnswering != null)
            {
                this.CurrentGame.PlayerAnswering.Score += this.CurrentGame.CurrentRound.Gameboard.CurrentFocus.Value;
                MySoundPlayer.PlayRightAnswerSound();
            }
            this.CurrentGame.PlayerAnswering = null;
            this.CurrentGame.CurrentRound.Gameboard.CurrentFocus = null;
        }

        private void OnWrongAnswer(object parameter = null)
        {
            if (this.CurrentGame.PlayerAnswering != null)
            {
                this.CurrentGame.PlayerAnswering.Score -= this.CurrentGame.CurrentRound.Gameboard.CurrentFocus.Value;
                this.CurrentGame.PlayerAnswering.HasAnswered = true;
            }
            this.CurrentGame.PlayerAnswering = null;
            MySoundPlayer.PlayWrongAnswerSound();


            // If everyone has answered - continue to next question
            bool hasEveryoneAnswered = true;
            foreach (var player in this.CurrentGame.Players)
            {
                if (!player.HasAnswered)
                {
                    hasEveryoneAnswered = false;
                    break;
                }
            }

            if (hasEveryoneAnswered)
            {
                OnNoAnswer(new object());

                // TODO: Show splash screen of answer
            }
        }

        private bool CanSelectResponse(object parameter = null)
        {
            return CurrentGame != null && CurrentGame.PlayerAnswering != null;
        }

        private void OnNoAnswer(object parameter = null)
        {
            if (parameter == null)
            {
                //Play sound if param is null
                MySoundPlayer.PlayNoAnswerSound();
            }

            this.CurrentGame.CurrentRound.Gameboard.CurrentFocus = null;
        }

        private bool CanSelectNoAnswer(object parameter = null)
        {
            return CurrentGame != null && CurrentGame.CurrentRound != null &&
                CurrentGame.CurrentRound.Gameboard != null &&
                CurrentGame.CurrentRound.Gameboard.CurrentFocus != null &&
                CurrentGame.PlayerAnswering == null &&
                CurrentGame.CurrentRound.Gameboard.CurrentFocus.DisplayType == DisplayType.Question;
        }


        private bool CanAnswer(object param = null)
        {
            if (this.CurrentGame != null)
            {
                return this.CurrentGame.PlayerAnswering != null;
            }
            return false;
        }

        private void OnSelectPlayer(object parameter)
        {
            Player player = parameter as Player;
            if (player != null)
            {
                if (this.CurrentGame.CurrentRound.Gameboard.CurrentFocus != null)
                {
                    this.CurrentGame.PlayerAnswering = player;
                    MySoundPlayer.PlayBuzzInAnswerSound();
                }
            }
        }

        private bool CanSelectPlayer(object parameter)
        {
            var player = parameter as Player;
            return CurrentGame != null && CurrentGame.CurrentRound != null && CurrentGame.CurrentRound.Gameboard != null &&
                CurrentGame.CurrentRound.Gameboard.CurrentFocus != null && CurrentGame.PlayerAnswering == null &&
                player != null && player.HasAnswered == false;
        }

        private void OnStartRound(object parameter = null)
        {
            if (CurrentGame != null && CurrentGame.CurrentRound != null && CurrentGame.CurrentRound.Gameboard != null)
            {
                CurrentGame.CurrentRound.Gameboard.CurrentFocus = null;

                // Show questions
                CurrentGame.CurrentRound.Gameboard.MakeQuestionsAppear();
                MySoundPlayer.PlayStartRoundSound();
                CurrentRoundStarted = true;

                //Show category headers

            }
        }

        private bool CanStartRound(object parameter = null)
        {
            if (CurrentGame != null && CurrentGame.CurrentRound != null && CurrentGame.CurrentRound.Gameboard != null &&
                CurrentRoundStarted == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnStartNextRound(object parameter = null)
        {
            if (CurrentGame.CurrentRound.RoundEnum == GameRoundEnum.RoundOne)
            {
                // in round one, go to round two
                CurrentGame.CurrentRound = CurrentGame.RoundTwo;

                foreach (var cat in CurrentGame.CurrentRound.Gameboard.Categories)
                {
                    foreach (var quest in cat.Questions)
                    {
                        quest.OnQuestionSelected = OnSelectQuestionCommand;
                    }
                }
                CurrentRoundStarted = false;
            }
            else if (CurrentGame.CurrentRound.RoundEnum == GameRoundEnum.RoundTwo)
            {
                // in round two, go to final jeopardy
            }
            else if (CurrentGame.CurrentRound.RoundEnum == GameRoundEnum.Final)
            {
                // end of game
            }
        }

        private bool CanStartNextRound(object parameter = null)
        {
            return CurrentRoundStarted;
        }

        public void ShowSpashScreen()
        {
            CurrentGame.CurrentRound.Gameboard.CurrentFocus = new JeopardyImage(Properties.Resources.JeopardySplash, "JeopardySplash.jpg");
        }
    }
}
