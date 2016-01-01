using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Jeopardy.Model;

namespace Jeopardy.View
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        private Timer timer1;

        bool MakeBigger = false;

        private SolidColorBrush brush1;

        private SolidColorBrush brush2;

        private Timer SecondScreenTimer;

        public bool GameEnded = false;

        public PlayerWindow(Game game)
        {
            this.Loaded += PlayerWindow_Loaded;

            InitializeComponent();
            CheckMonitor();
            //CheckMonitor();
            //double factor = 1.01;
            //System.Windows.Media.Color originalColour = Colors.White;
            //System.Windows.Media.Color adjustedColour = System.Windows.Media.Color.FromRgb(
            //    (byte)(originalColour.R * factor),
            //    (byte)(originalColour.G * factor),
            //    (byte)(originalColour.B * factor));

            //brush1 = new SolidColorBrush(adjustedColour);
            //brush2 = new SolidColorBrush(originalColour);

            DataContext = game;
            InitTimer();
        }

        void PlayerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Screen.AllScreens.Length > 1)
                this.WindowState = WindowState.Maximized;
            else
                this.Hide();

            CheckMonitor();
        }

        public void CheckMonitor()
        {
            CheckForSecondScreen(this, new EventArgs());

            // Have timer check for disconnect
            SecondScreenTimer = new Timer();
            SecondScreenTimer.Tick += new EventHandler(CheckForSecondScreen);
            SecondScreenTimer.Interval = 1000; // in miliseconds
            SecondScreenTimer.Start();
        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(Redraw);
            timer1.Interval = 100; // in miliseconds
            //timer1.Start();
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

        private void CheckForSecondScreen(object sender, EventArgs e)
        {
            if (GameEnded == true)
            {
                return;
            }

            if (Screen.AllScreens.Length > 1)
            {
                MoveToSecondaryScreen();
            }
            else
            {
                HidePlayerScreen();
            }
        }

        private void MoveToSecondaryScreen()
        {
            var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();

            var window = this;

            if (secondaryScreen != null)
            {
                if (!window.IsLoaded)
                    window.WindowStartupLocation = WindowStartupLocation.Manual;

                var workingArea = secondaryScreen.WorkingArea;
                window.Left = workingArea.Left;
                window.Top = workingArea.Top;
                window.Width = workingArea.Width;
                window.Height = workingArea.Height;
                window.WindowStyle = System.Windows.WindowStyle.None;
                // If window isn't loaded then maxmizing will result in the window displaying on the primary monitor
                if (window.IsLoaded)
                    window.WindowState = WindowState.Maximized;

                this.Show();
            }
        }

        private void HidePlayerScreen()
        {
            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
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
    }
}
