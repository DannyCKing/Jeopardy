
using System.Windows.Input;
namespace Jeopardy.Model.Interfaces
{
    public enum QuestionType { Text, Music, Image, Video, DailyDouble, CategoryHeader }

    public interface ISquare
    {
        QuestionType QuestionType { get; }

        int Rating { get; }

        string Source { get; }

        int Value { get; set; }

        bool IsCategoryHeader { get; }

        bool IsPlayed { get; set; }

        string Answer { get; }

        ICommand OnQuestionSelected { get; set; }
    }
}
