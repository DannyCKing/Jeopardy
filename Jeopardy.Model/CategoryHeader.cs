
using Jeopardy.Model.Interfaces;
namespace Jeopardy.Model
{
    public class CategoryHeader : ISquare
    {
        public QuestionType QuestionType
        {
            get { return QuestionType.CategoryHeader; }
        }

        public int Rating
        {
            get { return 0; }
        }

        public string Source
        {
            get;
            private set;
        }

        public int Value
        {
            get
            {
                return 0;
            }
            set
            {

            }
        }

        public bool IsCategoryHeader
        {
            get
            {
                return true;
            }

        }

        public bool IsPlayed
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        public CategoryHeader(string text)
        {
            Source = text;
        }


        public System.Windows.Input.ICommand OnQuestionSelected
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }


        public string Answer
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
