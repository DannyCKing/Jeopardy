using Jeopardy.Model;

namespace Jeopardy.ViewModel
{
    public class EditScoreViewModel
    {
        public Player Player { get; private set; }

        public int ScoreBefore { get; private set; }

        public EditScoreViewModel(Player player, int scoreBefore)
        {
            Player = player;
            ScoreBefore = scoreBefore;
        }
    }
}
