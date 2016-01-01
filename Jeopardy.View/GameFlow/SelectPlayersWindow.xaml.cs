using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Jeopardy.ViewModel;


namespace Jeopardy.View
{
    /// <summary>
    /// Interaction logic for SelectPlayersWindow.xaml
    /// </summary>
    public partial class SelectPlayersWindow : UserControl
    {
        public WrapperWindow WrapperWindow { get; protected set; }

        public SelectPlayersWindow(WrapperWindow wrapperWindow)
        {
            InitializeComponent();
            DataContext = new SelectPlayersViewModel();
            WrapperWindow = wrapperWindow;

            this.Loaded += SelectPlayersWindow_Loaded;
        }

        void SelectPlayersWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.First);
            MoveFocus(request);

            var items = Keyboard.FocusedElement as ItemsControl;
            request = new TraversalRequest(FocusNavigationDirection.Next);
            items.MoveFocus(request);

            IInputElement focusedControl = Keyboard.FocusedElement;
            if (focusedControl is TextBox)
            {
                var textBox = focusedControl as TextBox;
                if (textBox.Text.Length > 0)
                {
                    textBox.SelectionStart = textBox.Text.Length;
                    textBox.SelectionLength = 0;
                }
            }
        }

        private void Forward_Button_Click(object sender, RoutedEventArgs e)
        {
            this.WrapperWindow.ShowQuestionSelectorScreen1();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.WrapperWindow.ShowStartupWindow();
        }
    }
}
