using System;
using System.Collections.ObjectModel;
using System.IO;
using Jeopardy.Model;
using Jeopardy.Model.Interfaces;
using Jeopardy.Model.Helpers;

namespace Jeopardy.ViewModel
{
    public class QuestionPacksEditorViewModel : NotifyPropertyChanged
    {
        #region Properties

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

        public QuestionPacksEditorViewModel()
        {
            GetQuestionPacks();
        }

        #endregion

        #region Private Methods

        public void GetQuestionPacks()
        {
            QuestionPacks.Clear();

            foreach (string file in Directory.EnumerateFiles(Constants.APP_DATA_LOCATION, "*.xml"))
            {
                try
                {
                    var gameBoard = GetGameboardFromFile.GetGameboard(file);
                    QuestionPacks.Add(gameBoard);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(string.Format("{0} could not be read.  {1}", file, e.Message));
                }
            }

            if (QuestionPacks.Count == 0)
            {
                QuestionPacks.Add(new Gameboard());
            }
        }

        #endregion
    }
}
