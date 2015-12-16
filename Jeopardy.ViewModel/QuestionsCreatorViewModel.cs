using System;
using System.Windows;
using System.Windows.Input;
using Jeopardy.Model;
using Jeopardy.Model.Helpers;
using Jeopardy.ViewModel.Commands;

namespace Jeopardy.ViewModel
{
    public class QuestionsCreatorViewModel : NotifyPropertyChanged
    {
        #region Commands

        public ICommand SaveQuestionsCommand { get; private set; }

        #endregion

        #region Properties

        #region GameboardToSave

        private Gameboard _gameboardToSave;

        public Gameboard GameboardToSave
        {
            get
            {
                return _gameboardToSave;
            }
            set
            {
                _gameboardToSave = value;
                OnPropertyChanged("GameboardToSave");
            }
        }

        #endregion

        #endregion

        #region Constructors

        public QuestionsCreatorViewModel()
            : this(new Gameboard())
        {
        }

        public QuestionsCreatorViewModel(Gameboard gameboardToEdit)
        {
            GameboardToSave = gameboardToEdit;
            SaveQuestionsCommand = new ActionCommand(SaveQuestions);
        }

        #endregion

        #region Private Methods

        private void SaveQuestions(object parameter = null)
        {
            try
            {
                SaveGameboardToFile.SaveGameboard(GameboardToSave);
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(string.Format("There was an error in your xml.  {0}", ex.Message)) == MessageBoxResult.OK)
                {
                    return;
                }

            }
        }

        #endregion
    }
}
