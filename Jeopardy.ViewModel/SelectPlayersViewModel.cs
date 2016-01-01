using System.Collections.ObjectModel;
using System.Windows.Input;
using Jeopardy.Model;
using Jeopardy.Model.Interfaces;
using Jeopardy.ViewModel.Commands;

namespace Jeopardy.ViewModel
{
    public class SelectPlayersViewModel : NotifyPropertyChanged
    {

        #region Properties

        #region Players

        private ObservableCollection<Player> _players;

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

        #endregion

        #endregion

        #region Commands

        public ICommand AddPlayerCommand { get; private set; }

        public ICommand RemovePlayerCommand { get; private set; }

        #endregion

        #region Constructors

        public SelectPlayersViewModel()
        {
            AddPlayerCommand = new RelayCommand(AddPlayer, CanAddPlayer);
            RemovePlayerCommand = new RelayCommand(RemovePlayer, CanRemovePlayer);

            AddPlayer();
            AddPlayer();
        }

        #endregion

        #region Private methods

        public void AddPlayer(object parameter = null)
        {
            Players.Add(new Player(TeamNames.GetRandomTeamName(Players)));
        }

        private bool CanAddPlayer(object parameter = null)
        {
            if (Players.Count < 4)
                return true;
            else
                return false;
        }

        private bool CanRemovePlayer(object obj)
        {
            return Players.Count > 1;
        }

        private void RemovePlayer(object parameter)
        {
            var player = parameter as Player;
            if (player != null)
            {
                Players.Remove(player);
            }
        }

        #endregion
    }
}
