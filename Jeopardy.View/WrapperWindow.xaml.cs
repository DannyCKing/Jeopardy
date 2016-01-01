using System.Collections.Generic;
using System.Windows;
using Jeopardy.Model;
using Jeopardy.View.GameFlow;

namespace Jeopardy.View
{
    /// <summary>
    /// Interaction logic for WrapperWindow.xaml
    /// </summary>
    public partial class WrapperWindow : Window
    {
        public StartupWindow StartupWindow { get; protected set; }
        public SelectPlayersWindow SelectPlayersWindow { get; protected set; }
        public SelectQuestionsWindow SelectQuestionsWindow { get; protected set; }
        public AdminWindow HostWindow { get; protected set; }
        public QuestionPacksEditor QuestionsEditorWindow { get; protected set; }

        public WrapperWindow()
        {
            StartupWindow = new StartupWindow(this);
            SelectPlayersWindow = new SelectPlayersWindow(this);
            SelectQuestionsWindow = new SelectQuestionsWindow(this);
            QuestionsEditorWindow = new QuestionPacksEditor(this);
            InitializeComponent();

            ContentControl.Content = StartupWindow;
        }

        public void ShowStartupWindow()
        {
            ContentControl.Content = StartupWindow;
            SelectPlayersWindow = new SelectPlayersWindow(this);
            SelectQuestionsWindow = new SelectQuestionsWindow(this);
            QuestionsEditorWindow = new QuestionPacksEditor(this);
            if (HostWindow != null)
            {

            }
        }

        public void ShowPlayerSelectorScreen()
        {
            ContentControl.Content = SelectPlayersWindow;
        }

        public void ShowQuestionSelectorScreen1()
        {
            ContentControl.Content = SelectQuestionsWindow;
        }

        public void ShowPlayWindow()
        {
            List<Player> players = new List<Player>();
            var playersFromPlayScreen = SelectPlayersWindow.PlayersControl.ItemsSource;
            foreach (var player in playersFromPlayScreen)
            {
                var playerToAdd = player as Player;
                players.Add(playerToAdd);
            }
            var board1 = SelectQuestionsWindow.QuestionPacks1.SelectedItem as Gameboard;
            var board2 = SelectQuestionsWindow.QuestionPacks2.SelectedItem as Gameboard;
            HostWindow = new AdminWindow(new Game(players, board1, board2), this);
            ContentControl.Content = HostWindow;
        }

        public void ShowWriteQuestionsScreen()
        {
            ContentControl.Content = QuestionsEditorWindow;
        }
    }
}
