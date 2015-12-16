using System;
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

        private SolidColorBrush brush1;

        private SolidColorBrush brush2;

        public PlayerWindow(Game game)
        {
            this.Loaded += PlayerWindow_Loaded;
            double factor = 1.01;
            System.Windows.Media.Color originalColour = Colors.White;
            System.Windows.Media.Color adjustedColour = System.Windows.Media.Color.FromRgb(
                (byte)(originalColour.R * factor),
                (byte)(originalColour.G * factor),
                (byte)(originalColour.B * factor));

            brush1 = new SolidColorBrush(adjustedColour);
            brush2 = new SolidColorBrush(originalColour);

            DataContext = game;
            InitializeComponent();
            InitTimer();
        }

        void PlayerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(Refresh);
            timer1.Interval = 100; // in miliseconds
            timer1.Start();
        }

        private void Refresh(object sender, EventArgs e)
        {
            //Background = brush1;
            //Background = brush2;
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
