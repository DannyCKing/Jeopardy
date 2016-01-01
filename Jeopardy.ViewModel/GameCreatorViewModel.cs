using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Jeopardy.Model;
using Jeopardy.Model.Helpers;
using Jeopardy.Model.Interfaces;
using Jeopardy.ViewModel.Commands;

namespace Jeopardy.ViewModel
{
    public class GameCreatorViewModel : NotifyPropertyChanged
    {
        #region Fields/Properties

        public Game CreatedGame { get; set; }

        private ObservableCollection<Player> _players;

        public ICommand AddPlayerCommand { get; private set; }

        public ICommand RemovePlayerCommand { get; private set; }

        public ICommand CreateGameCommand { get; private set; }

        public ICommand CreateQuestionsCommand { get; private set; }

        #region Question Packs

        private ObservableCollection<Gameboard> _questionPacksRoundOne;

        public ObservableCollection<Gameboard> QuestionPacksRoundOne
        {
            get
            {
                if (_questionPacksRoundOne == null)
                {
                    _questionPacksRoundOne = new ObservableCollection<Gameboard>();
                }
                return _questionPacksRoundOne;
            }
            set
            {
                _questionPacksRoundOne = value;
                OnPropertyChanged("QuestionPacksRoundOne");
            }
        }

        private ObservableCollection<Gameboard> _questionPacksRoundTwo;

        public ObservableCollection<Gameboard> QuestionPacksRoundTwo
        {
            get
            {
                if (_questionPacksRoundTwo == null)
                {
                    _questionPacksRoundTwo = new ObservableCollection<Gameboard>();
                }
                return _questionPacksRoundTwo;
            }
            set
            {
                _questionPacksRoundTwo = value;
                OnPropertyChanged("QuestionPacksRoundTwo");
            }
        }

        #endregion

        private Gameboard _roundOneSelectedItem;

        public Gameboard RoundOneSelectedItem
        {
            get
            {
                return _roundOneSelectedItem;
            }
            set
            {
                _roundOneSelectedItem = value;
                OnPropertyChanged("RoundOneSelectedItem");
            }
        }

        private Gameboard _roundTwoSelectedItem;

        public Gameboard RoundTwoSelectedItem
        {
            get
            {
                return _roundTwoSelectedItem;
            }
            set
            {
                _roundTwoSelectedItem = value;
                OnPropertyChanged("RoundTwoSelectedItem");
            }
        }

        private string _errorText = "";

        public string ErrorText
        {
            get
            {
                return _errorText;
            }
            private set
            {
                _errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }

        #endregion

        #region Constructors

        public GameCreatorViewModel()
        {
            GameStarted = false;

            Players = new ObservableCollection<Player> { new Player(TeamNames.GetRandomTeamName(Players)) };
            Players.Add(new Player(TeamNames.GetRandomTeamName(Players)));

            AddPlayerCommand = new ActionCommand(AddPlayer);
            RemovePlayerCommand = new ActionCommand(RemovePlayer);

            CreateGameCommand = new ActionCommand(CreateGame);

            GetQuestionPacks();
        }


        #endregion

        public ObservableCollection<Player> Players
        {
            get
            {
                if (_players == null)
                {
                    _players = new ObservableCollection<Player>();
                }
                return _players;
            }
            set
            {
                _players = value;
                OnPropertyChanged("Players");
            }
        }

        public void GetQuestionPacks()
        {
            QuestionPacksRoundOne.Clear();

            QuestionPacksRoundOne = GamePacksReader.GetSavedGameBoards();

            QuestionPacksRoundTwo.Clear();

            QuestionPacksRoundTwo = GamePacksReader.GetSavedGameBoards();

            if (QuestionPacksRoundOne.Count == 0)
            {
                QuestionPacksRoundOne.Add(new Gameboard());
            }

            if (QuestionPacksRoundTwo.Count == 0)
            {
                QuestionPacksRoundTwo.Add(new Gameboard());
            }
        }

        private void AddPlayer(object parameter = null)
        {
            Players.Add(new Player(TeamNames.GetRandomTeamName(Players)));
        }

        private void RemovePlayer(object parameter = null)
        {
            if (Players.Count > 0)
            {
                Players.RemoveAt(Players.Count - 1);
            }
        }

        private void CreateGame(object parameter)
        {
            Gameboard one = RoundOneSelectedItem;
            Gameboard two = RoundTwoSelectedItem;
            if (one == null || two == null)
            {
                ErrorText = "Select a game for round one and round two";
                return;
            }
            CreatedGame = new Game(GetPlayers(), one, two);
            GameStarted = true;
            Window window = parameter as Window;
            window.DialogResult = true;
            window.Close();
        }

        private Gameboard GetGameboard(string fileLocation)
        {
            try
            {
                return GetGameboardFromFile.GetGameboard(fileLocation);
            }
            catch (Exception e)
            {
                ErrorText = e.Message;
            }
            return null;
        }

        private List<Player> GetPlayers()
        {
            List<Player> players = new List<Player>();
            foreach (var player in Players)
            {
                players.Add(player);
            }
            return players;
        }

        public bool GameStarted { get; set; }
    }
}
