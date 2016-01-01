using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Jeopardy.Model;
using Jeopardy.Model.Helpers;
using Jeopardy.Model.Interfaces;
using Jeopardy.ViewModel.Commands;

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

        #region CurrentQuestionPack

        private Gameboard _currentQuestionPack;

        public Gameboard CurrentQuestionPack
        {
            get
            {
                return _currentQuestionPack;
            }
            set
            {
                _currentQuestionPack = value;
                OnPropertyChanged("CurrentQuestionPack");
            }
        }

        #region Current Category

        private Category _currentCategory;

        public Category CurrentCategory
        {
            get
            {
                return _currentCategory;
            }
            set
            {
                _currentCategory = value;
                OnPropertyChanged("CurrentCategory");
            }
        }

        #endregion

        #endregion

        #endregion

        #region Commands

        public ICommand AddGameboardCommand { get; private set; }

        public ICommand SaveGameboardsCommand { get; private set; }

        public ICommand DeleteGameboardsCommand { get; private set; }

        #endregion

        #region Constructors

        public QuestionPacksEditorViewModel()
        {
            GetQuestionPacks();
            AddGameboardCommand = new ActionCommand(AddNewQuestionPack);
            SaveGameboardsCommand = new ActionCommand(OnSaveGameboards);
            DeleteGameboardsCommand = new ActionCommand(OnDeleteGameboard);
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

            if (QuestionPacks.Count() == 0)
            {
                QuestionPacks.Add(new Gameboard(true));
            }
        }

        private void OnDeleteGameboard(object param = null)
        {
            OnSaveGameboards();

            var gameboard = param as Gameboard;
            if (gameboard != null)
            {
                string warning = string.Format("Are you sure you want to delete the question pack {0}?  This CAN NOT be undone!", gameboard.QuestionPackName);
                string caption = "Are you sure?";
                if (MessageBox.Show(warning, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        DeleteGameboardFile.DeleteGamebordFile(gameboard);
                        GetQuestionPacks();
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show(string.Format("There was an error in deleting the question pack {0}.", gameboard.QuestionPackName)) == MessageBoxResult.OK)
                        {
                            return;
                        }
                    }
                }
            }

        }

        private void AddNewQuestionPack(object param = null)
        {
            var gameboard = new Gameboard();
            int count = 0;
            var orginalName = gameboard.QuestionPackName;
            while (QuestionPacks.FirstOrDefault(x => x.QuestionPackName == gameboard.QuestionPackName) != null)
            {
                count++;
                gameboard.QuestionPackName = orginalName + " " + count;
            }
            QuestionPacks.Add(gameboard);
        }

        private void OnSaveGameboards(object parameter = null)
        {
            foreach (var gameboard in QuestionPacks)
            {
                try
                {
                    SaveGameboardToFile.SaveGameboard(gameboard);
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(string.Format("There was an error in your xml for the question pack {0}.  {1}", gameboard.QuestionPackName, ex.Message)) == MessageBoxResult.OK)
                    {
                        return;
                    }
                }
            }
        }

        #endregion
    }
}
