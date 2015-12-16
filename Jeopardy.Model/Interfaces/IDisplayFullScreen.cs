
using System.Drawing;
namespace Jeopardy.Model.Interfaces
{
    public enum DisplayType { Question, Category, Picture }

    public interface IDisplayFullScreen
    {
        DisplayType DisplayType { get; }

        string Source { get; }

        int Value { get; }

        bool IsPlayed { get; set; }

        string Answer { get; set; }

        Color BackgroundColor { get; set; }
    }
}
