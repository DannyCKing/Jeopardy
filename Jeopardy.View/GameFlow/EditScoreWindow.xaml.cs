using System.Windows;
using Jeopardy.Model;
using Jeopardy.ViewModel;

namespace Jeopardy.View.GameFlow
{
    /// <summary>
    /// Interaction logic for EditScoreWindow.xaml
    /// </summary>
    public partial class EditScoreWindow : Window
    {
        public EditScoreWindow(Player player)
        {
            DataContext = new EditScoreViewModel(player, player.Score);
            InitializeComponent();
        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            var context = DataContext as EditScoreViewModel;
            if (context != null)
            {
                context.Player.Score = context.ScoreBefore;
            }
            this.Close();
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
