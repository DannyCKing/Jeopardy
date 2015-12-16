
namespace Jeopardy.Model
{
    public enum GameRoundEnum { RoundOne, RoundTwo, Final };

    public class Round
    {
        public Gameboard Gameboard { get; private set; }

        public GameRoundEnum RoundEnum { get; private set; }

        public string Name { get; private set; }

        public int Multiplier { get; private set; }

        public Round(Gameboard gameBoard, string name, int multiplier, GameRoundEnum gameEnum)
        {
            Gameboard = gameBoard;
            Name = name;
            Multiplier = multiplier;
            foreach (var cat in Gameboard.Categories)
            {
                foreach (var quest in cat.Questions)
                {
                    quest.Value = quest.Rating * Multiplier;
                }
            }
            RoundEnum = gameEnum;
        }
    }
}
