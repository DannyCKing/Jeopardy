using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Jeopardy.ViewModel;

namespace Jeopardy.View.GameFlow
{
    /// <summary>
    /// Interaction logic for SelectQuestionsWindow.xaml
    /// </summary>
    public partial class SelectQuestionsWindow : System.Windows.Controls.UserControl
    {
        private readonly WrapperWindow WrapperWindow;


        public SelectQuestionsWindow(WrapperWindow window)
        {
            DataContext = new SelectQuestionsViewModel();
            InitializeComponent();
            WrapperWindow = window;
        }

        private void Forward_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Screen.AllScreens.Length > 1)
            {
                this.WrapperWindow.ShowPlayWindow();
            }
            else
            {
                string warning = "You must be connected to a second monitor to play this game.  You can continue for testing your questions only.  " +
                    "  If you wish to play the game, connect to a second monitor now.  When connected, click okay.";

                string caption = "Connect to second monitor";
                if (System.Windows.MessageBox.Show(warning, caption, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    this.WrapperWindow.ShowPlayWindow();
                }
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.WrapperWindow.ShowPlayerSelectorScreen();
        }
    }
}
