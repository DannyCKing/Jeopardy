
using Jeopardy.Model.Interfaces;
using Jeopardy.ViewModel;
namespace Jeopardy.Model
{
    public class Category : NotifyPropertyChanged
    {
        #region Constants

        private const int QUESTION_PER_CATEGORY = 5;

        public const string CATEGORY_NAME = "CATEGORY {0} TITLE";

        public const string QUESTION_FORMAT = "Question for question {0} - Visible to players";

        public const string ANSWER_FORMAT = "Answer for question {0} - Visible to host only";

        #endregion

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

        public static Category[] GetPlaceholderCategories()
        {
            return new Category[] { 
                GetCategory(1), GetCategory(2), GetCategory(3), GetCategory(4), GetCategory(5), GetCategory(6)};
        }

        private static Question GetQuestion(int value)
        {
            return new Question(string.Format(QUESTION_FORMAT, value),
                string.Format(ANSWER_FORMAT, value), value);
        }

        private static Category GetCategory(int categoryNumber)
        {
            return new Category(string.Format(CATEGORY_NAME, categoryNumber),
                GetQuestion(1), GetQuestion(2), GetQuestion(3), GetQuestion(4), GetQuestion(5));
        }

        public static Category[] GetExampleCategories()
        {
            var Categories = new Category[] { 
                new Category("Math", 
                    new Question("What is 4 x 2?", "8", 1),
                    new Question("What is the approximate value of pi?", "3.1415", 2),
                    new Question("What is -4 x -4", "16", 3),
                    new Question("What is the square root of 64?", "8", 4),
                    new Question("What is 12 x 12?", "144", 5)),
                new Category("Social Studies", 
                    new Question("What is the capital of the US?", "Washington D.C.", 1),
                    new Question("This is the capital of California", "Sacramento", 2),
                    new Question("This is the capital of Nevada", "Carson City", 3),
                    new Question("This is the largest city(in terms of population) in the US", "New York City", 4),
                    new Question("This is the capital of Idaho", "Boise", 5)),
                new Category("Star Wars", 
                    new Question("Who is Luke's Father?", "Darth Vader", 1),
                    new Question("How are Luke and Leia related?", "They're siblings", 2),
                    new Question("What color is Luke's lightsaber in in Episode VI", "Green", 3),
                    new Question("What color is Vader's lightsaber?", "Red", 4),
                    new Question("Who killed Mace Windu?", "The Emperor", 5)),
                new Category("Presidents", 
                    new Question("Who killed Abraham Lincoln?", "John Wilkes Booth", 1),
                    new Question("This president is the 'Father of our country'", "George Washington", 2),
                    new Question("This president was a peanut farmer", "Jimmy Carter", 3),
                    new Question("This president was famous for acting", "Ronald Reagan", 4),
                    new Question("This man was the second president of the USA", "John Adams", 5)),
                new Category("Computers", 
                    new Question("CPU is an acronym for this piece of hardware", "The Central Processing Unit", 1),
                    new Question("1 kilobyte is about 1000 bytes.  1 gigabyte is this many", "1 billion", 2),
                    new Question("A text file usually has this suffix in the filename", ".txt", 3),
                    new Question("RAM stands for this", "Random Access Memory", 4),
                    new Question("A byte consist of this many bits", "8", 5)),
                new Category("Star Trek", 
                    new Question("What is the nickname for the doctor in the original Star Trek", "Bones", 1),
                    new Question("This captains drink of choice is 'Tea, Earl Gray, Hot'", "Captain Picard", 2),
                    new Question("'You will be assimilated' is the tag phrase from this species", "The Borg", 3),
                    new Question("Who is the captian in Deep Space Nine", "Sisko", 4),
                    new Question("Who is the all powerful being in the Next Generation?", "Q", 5))};
            return Categories;
        }
    }
}
