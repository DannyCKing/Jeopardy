
using Jeopardy.Model.Interfaces;
using Jeopardy.ViewModel;
namespace Jeopardy.Model
{
    public class Category : NotifyPropertyChanged
    {
        private const int QUESTION_PER_CATEGORY = 5;

        private string _categoryTextHidden;

        private string _categoryName;

        public string CategoryName
        {
            get
            {
                if (ShowTitle)
                {
                    return _categoryName.ToUpper();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                _categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        private bool _showTitle = true;

        public bool ShowTitle
        {
            get
            {
                return _showTitle;
            }
            set
            {
                _showTitle = value;
                OnPropertyChanged("ShowTitle");
            }
        }

        public ISquare[] Questions { get; private set; }


        public Category(string catergoryName, ISquare question1, ISquare question2, ISquare question3, ISquare question4, ISquare question5)
        {
            _categoryTextHidden = catergoryName;
            CategoryName = catergoryName;
            Questions = new ISquare[] { question1, question2, question3, question4, question5 };
        }
    }
}
