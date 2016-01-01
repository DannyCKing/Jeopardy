using System;
using System.Windows.Forms;
using System.Windows.Media;
using Jeopardy.Model;
using Jeopardy.View.GameFlow;
using Jeopardy.ViewModel;

namespace Jeopardy.View
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : System.Windows.Controls.UserControl
    {
        public PlayerWindow childWindow;

        public WrapperWindow WrapperWindow { get; private set; }

        bool MakeBigger = false;

        private Timer timer1;

        private SolidColorBrush brush1;

        private SolidColorBrush brush2;

        public AdminWindow(Game game, WrapperWindow wrapperWindow)
        {
            WrapperWindow = wrapperWindow;

            InitializeComponent();

            this.DataContext = new GameAdminViewModel(game);

            childWindow = new PlayerWindow(game);


            //if (Screen.AllScreens.Length > 1)
            //{
            //    KeyGesture OpenKeyGesture = new KeyGesture(
            //                                 Key.Left,
            //                                 ModifierKeys.Shift | ModifierKeys.Alt);
            //    KeyBinding OpenCmdKeybinding = new KeyBinding(
            //        ApplicationCommands.,
            //        OpenKeyGesture);
            //    this.InputBindings.Add(OpenCmdKeybinding);
            //}
            //InitTimer();

            //double factor = 1.01;
            //System.Windows.Media.Color originalColour = Colors.White;
            //System.Windows.Media.Color adjustedColour = System.Windows.Media.Color.FromRgb(
            //    (byte)(originalColour.R * factor),
            //    (byte)(originalColour.G * factor),
            //    (byte)(originalColour.B * factor));

            //brush1 = new SolidColorBrush(adjustedColour);
            //brush2 = new SolidColorBrush(originalColour);

        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(Redraw);
            timer1.Interval = 1000; // in miliseconds
            //timer1.Start();
        }

        private Screen GetSecondaryScreen()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen != Screen.PrimaryScreen)
                    return screen;
            }
            return Screen.PrimaryScreen;
        }

        void adminViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Redraw(sender, new EventArgs());
        }

        private void Redraw(object sender, EventArgs e)
        {
            if (this.MakeBigger)
            {
                this.Width = this.Width + 1;
                this.MakeBigger = false;
            }
            else
            {
                this.Width = this.Width - 1;
                this.MakeBigger = true;
            }
        }

        private void On_Window_Closed(object sender, EventArgs e)
        {
            //System.Windows.Application.Current.Shutdown();
        }

        private void On_QuitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Pop up warning -
            childWindow.GameEnded = true;
            childWindow.Close();
            WrapperWindow.ShowStartupWindow();
        }

        private void OnEditScoreClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var player = (Player)((System.Windows.Controls.Button)sender).Tag;
            var window = new EditScoreWindow(player);
            window.ShowDialog();

        }
    }
}
