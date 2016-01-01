
using System.Windows.Input;
using Jeopardy.ViewModel;
namespace Jeopardy.Model
{
    public class Player : NotifyPropertyChanged
    {
        #region Properties

        #region Name

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        #endregion

        #region Score

        private int _score;

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        #endregion

        #region IsAnswering

        private bool _isAnswering;

        public bool IsAnswering
        {
            get
            {
                return _isAnswering;
            }
            set
            {
                _isAnswering = value;
                OnPropertyChanged("IsAnswering");
            }
        }

        #endregion

        #region HasAnswered

        private bool _hasAnsweredCurrentQuestion;

        public bool HasAnswered
        {
            get
            {
                return _hasAnsweredCurrentQuestion;
            }
            set
            {
                _hasAnsweredCurrentQuestion = value;
                OnPropertyChanged("HasAnswered");
            }
        }

        #endregion

        #endregion

        public ICommand OnSelectPlayerCommand { get; set; }

        public Player(string name)
        {
            Score = 0;
            Name = name;
        }
    }
}
