
using System.Drawing;
using System.Windows.Input;
using Jeopardy.Model.Interfaces;
using Jeopardy.ViewModel;
namespace Jeopardy.Model
{
    public class Question : NotifyPropertyChanged, IDisplayFullScreen, ISquare
    {
        public QuestionType QuestionType { get; private set; }

        private string _source;

        public string Source
        {
            get
            {
                return _source.ToUpper();
            }
            set
            {
                _source = value;
                OnPropertyChanged("Source");
            }
        }

        private string _answer;
        public string Answer
        {
            get
            {
                return _answer;
            }
            set
            {
                _answer = value;
                OnPropertyChanged("Answer");
            }
        }

        public int Rating { get; private set; }

        public string Author { get; private set; }

        private int _value;

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
                OnPropertyChanged("ValueAmount");
            }
        }

        public string ValueAmount
        {
            get
            {
                return "$" + Value;
            }
        }

        public Question(string text, string answer, int rating, string author = null)
        {
            Source = text;
            Rating = rating;
            Answer = answer;
            Author = author;

            // Start question out as is played so we can animate them appearing like Jeopardy
            IsPlayed = true;
        }

        public bool IsCategoryHeader
        {
            get { return false; }
        }

        private bool isPlayed;

        public bool IsPlayed
        {
            get
            {
                return isPlayed;
            }
            set
            {
                isPlayed = value;
                OnPropertyChanged("IsPlayed");
            }
        }

        private ICommand _onquestionSelected;

        public ICommand OnQuestionSelected
        {
            get
            {
                return _onquestionSelected;
            }
            set
            {
                _onquestionSelected = value;
                OnPropertyChanged("OnQuestionSelected");
            }
        }

        public DisplayType DisplayType
        {
            get
            {
                return DisplayType.Question;
            }
        }

        private System.Drawing.Color _backgroundColor;

        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;
                OnPropertyChanged("BackgroundColor");
            }
        }
    }
}
