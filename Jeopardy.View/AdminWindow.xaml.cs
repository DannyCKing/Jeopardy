using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Jeopardy.ViewModel;

namespace Jeopardy.View
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        PlayerWindow childWindow;

        private Timer timer1;

        private SolidColorBrush brush1;

        private SolidColorBrush brush2;

        public AdminWindow()
        {
            InitializeComponent();

            this.Loaded += AdminWindow_Loaded;

            double factor = 1.01;
            System.Windows.Media.Color originalColour = Colors.White;
            System.Windows.Media.Color adjustedColour = System.Windows.Media.Color.FromRgb(
                (byte)(originalColour.R * factor),
                (byte)(originalColour.G * factor),
                (byte)(originalColour.B * factor));

            brush1 = new SolidColorBrush(adjustedColour);
            brush2 = new SolidColorBrush(originalColour);

        }

        void AdminWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitTimer();

            GameCreatorWindow gameCreator = new GameCreatorWindow();
            if (gameCreator.ShowDialog() == true)
            {
                var creatorViewModel = gameCreator.DataContext as GameCreatorViewModel;
                var adminViewModel = DataContext as GameAdminViewModel;
                adminViewModel.CurrentGame = creatorViewModel.CreatedGame;
                adminViewModel.StartGameCommand.Execute(null);
                adminViewModel.ShowSpashScreen();


                var game = adminViewModel.CurrentGame;
                childWindow = new PlayerWindow(game);

                if (Screen.AllScreens.Length > 1)
                {
                    childWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                    System.Drawing.Rectangle workingArea = GetSecondaryScreen().WorkingArea;
                    childWindow.Left = workingArea.Left;
                    childWindow.Top = workingArea.Top;
                    childWindow.Width = workingArea.Width;
                    childWindow.Height = workingArea.Height;
                    //childWindow.WindowState = WindowState.Maximized;
                    childWindow.WindowStyle = WindowStyle.None;
                    //childWindow.Topmost = true;
                }
                childWindow.Show();


            }
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

        private void On_Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
