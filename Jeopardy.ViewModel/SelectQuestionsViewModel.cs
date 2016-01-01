using System.Collections.ObjectModel;
using Jeopardy.Model;
using Jeopardy.Model.Helpers;

namespace Jeopardy.ViewModel
{
    public class SelectQuestionsViewModel : NotifyPropertyChanged
    {
        #region Properties

        public Gameboard Gameboard { get; private set; }

        #region Question Packs

        private ObservableCollection<Gameboard> _questionPacks;

        public ObservableCollection<Gameboard> QuestionPacks
        {
            get
            {
                if (_questionPacks == null)
                {
                    _questionPacks = new ObservableCollection<Gameboard>();
                }
                return _questionPacks;
            }
            set
            {
                _questionPacks = value;
                OnPropertyChanged("QuestionPacks");
            }
        }

        #endregion

        #endregion

        #region Constructors

        public SelectQuestionsViewModel()
        {
            QuestionPacks = new ObservableCollection<Gameboard>();
            LoadQuestionPacks();
        }

        #endregion

        #region Private Methods

        private void LoadQuestionPacks()
        {
            QuestionPacks.Clear();

            QuestionPacks = GamePacksReader.GetSavedGameBoards();
        }

        #endregion
    }
}
