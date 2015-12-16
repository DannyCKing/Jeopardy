
using System;
using System.Collections.Generic;
using System.Timers;
using Jeopardy.Model.Interfaces;
using Jeopardy.ViewModel;
namespace Jeopardy.Model
{
    public class Gameboard : NotifyPropertyChanged
    {
        private const int CATEGORIES_PER_GAME = 6;

        public Guid UniqueIdentifier
        {
            get;
            private set;
        }

        private List<Tuple<int, int>> QuestionAppearanceOrder
        {
            get;
            set;
        }

        private Timer Timer
        {
            get;
            set;
        }

        private static Random rng = new Random();

        #region QuestionPackName

        private string _questionPackname;

        public string QuestionPackName
        {
            get
            {
                return _questionPackname;
            }
            set
            {
                _questionPackname = value;
                OnPropertyChanged("QuestionPackName");
            }
        }

        #endregion

        #region Categories

        private Category[] _categories;

        public Category[] Categories
        {
            get
            {
                return _categories;
            }
            private set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        #endregion

        #region CurrentFocus

        private IDisplayFullScreen _currentFocus;

        public IDisplayFullScreen CurrentFocus
        {
            get
            {
                return _currentFocus;
            }
            set
            {
                _currentFocus = value;
                OnPropertyChanged("CurrentFocus");
            }
        }

        #endregion

        #region Constructors

        public Gameboard()
        {
            UniqueIdentifier = Guid.NewGuid();
            QuestionPackName = "Sample Pack";
            Categories = new Category[] { 
                new Category("Category 1 Name", 
                    new Question("Question 1", "Answer 1", 1),
                    new Question("Question 2", "Answer 2", 2),
                    new Question("Question 3", "Answer 3", 3),
                    new Question("Question 4", "Answer 4", 4),
                    new Question("Question 5", "Answer 5", 5)),
                new Category("Category 2 Name", 
                    new Question("Question 1", "Answer 1", 1),
                    new Question("Question 2", "Answer 2", 2),
                    new Question("Question 3", "Answer 3", 3),
                    new Question("Question 4", "Answer 4", 4),
                    new Question("Question 5", "Answer 5", 5)),
                new Category("Category 3 Name", 
                    new Question("Question 1", "Answer 1", 1),
                    new Question("Question 2", "Answer 2", 2),
                    new Question("Question 3", "Answer 3", 3),
                    new Question("Question 4", "Answer 4", 4),
                    new Question("Question 5", "Answer 5", 5)),
                new Category("Category 4 Name", 
                    new Question("Question 1", "Answer 1", 1),
                    new Question("Question 2", "Answer 2", 2),
                    new Question("Question 3", "Answer 3", 3),
                    new Question("Question 4", "Answer 4", 4),
                    new Question("Question 5", "Answer 5", 5)),
                new Category("Category 5 Name", 
                    new Question("Question 1", "Answer 1", 1),
                    new Question("Question 2", "Answer 2", 2),
                    new Question("Question 3", "Answer 3", 3),
                    new Question("Question 4", "Answer 4", 4),
                    new Question("Question 5", "Answer 5", 5)),
                new Category("Category 6 Name", 
                    new Question("Question 1", "Answer 1", 1),
                    new Question("Question 2", "Answer 2", 2),
                    new Question("Question 3", "Answer 3", 3),
                    new Question("Question 4", "Answer 4", 4),
                    new Question("Question 5", "Answer 5", 5))};
        }

        public Gameboard(string roundName, Guid guid, Category cat1, Category cat2, Category cat3, Category cat4, Category cat5, Category cat6)
        {
            UniqueIdentifier = guid;
            Categories = new Category[] { cat1, cat2, cat3, cat4, cat5, cat6 };
            QuestionPackName = roundName;
        }

        #endregion

        public void MakeQuestionsAppear()
        {
            SetOrderOfQuestionAppearance();

            Timer = new Timer();
            Timer.Enabled = true;
            Timer.Elapsed += timer1_Elapsed;
            Timer.Interval = 120; // in miliseconds

            Timer.Start();
        }

        private void SetOrderOfQuestionAppearance()
        {
            int max_width = Categories.Length;
            int max_height = Categories[0].Questions.Length;

            QuestionAppearanceOrder = new List<Tuple<int, int>>();

            // all questions to list;
            for (int i = 0; i < max_width; i++)
            {
                for (int j = 0; j < max_height; j++)
                {
                    QuestionAppearanceOrder.Add(Tuple.Create(i, j));
                }
            }

            // Shuffle list
            ShuffleList(QuestionAppearanceOrder);
        }

        private void ShuffleList(List<Tuple<int, int>> orderedList)
        {
            int n = orderedList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = orderedList[k];
                orderedList[k] = orderedList[n];
                orderedList[n] = value;
            }
        }

        private void timer1_Elapsed(object sender, EventArgs e)
        {
            int questionsInInterval = 1;
            for (int i = 0; i < questionsInInterval; i++)
            {
                ShowAtFirstIndex();
            }
        }

        private void ShowAtFirstIndex()
        {
            if (QuestionAppearanceOrder.Count > 0)
            {
                var item = QuestionAppearanceOrder[0];
                Categories[item.Item1].Questions[item.Item2].IsPlayed = false;
                QuestionAppearanceOrder.RemoveAt(0);
            }
            else
            {
                Timer.Stop();
                Timer.Enabled = false;
            }
        }

    }
}
