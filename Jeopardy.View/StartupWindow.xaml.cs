using System.Windows.Controls;
using System.Windows.Input;

namespace Jeopardy.View
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : UserControl
    {
        WrapperWindow WrapperWindow;

        public StartupWindow(WrapperWindow wrapperWindow)
        {
            WrapperWindow = wrapperWindow;
            InitializeComponent();

        }

        private void On_Exit_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void On_Play_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            this.WrapperWindow.ShowPlayerSelectorScreen();
        }

        private void On_Write_Questions_Clicked(object sender, MouseButtonEventArgs e)
        {
            this.WrapperWindow.ShowWriteQuestionsScreen();
        }
    }
}
