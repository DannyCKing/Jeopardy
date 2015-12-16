using System.Collections.Generic;
using Jeopardy.ViewModel;

namespace Jeopardy.Model
{
    public class Game : NotifyPropertyChanged
    {
        private List<Player> _players;

        public List<Player> Players
        {
            get
            {
                return _players;
            }
            set
            {
                _players = value;
                OnPropertyChanged("Players");
            }
        }

        public Round RoundOne { get; private set; }

        public Round RoundTwo { get; private set; }

        private Round _currentRound;

        public Round CurrentRound
        {
            get
            {
                return _currentRound;
            }
            set
            {
                _currentRound = value;
                OnPropertyChanged("CurrentRound");
            }
        }


        private Player _playerAnswering;

        public Player PlayerAnswering
        {
            get
            {
                return _playerAnswering;
            }
            set
            {
                if (_playerAnswering != null)
                {
                    _playerAnswering.IsAnswering = false;
                }
                _playerAnswering = value;
                if (_playerAnswering != null)
                {
                    _playerAnswering.IsAnswering = true;
                }

                OnPropertyChanged("PlayerAnswering");
            }
        }

        public Game(List<Player> players, Gameboard roundOne, Gameboard roundTwo)
        {
            Players = players;
            RoundOne = new Round(roundOne, "Jeopardy", 100, GameRoundEnum.RoundOne);
            RoundTwo = new Round(roundTwo, "Double Jeopardy", 200, GameRoundEnum.RoundTwo);
            CurrentRound = RoundOne;
        }
    }
}
