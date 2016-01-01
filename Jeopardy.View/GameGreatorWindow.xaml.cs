using System;
using System.Windows;
using Jeopardy.ViewModel;

namespace Jeopardy.View
{
    /// <summary>
    /// Interaction logic for GameCreatorWindow.xaml
    /// </summary>
    public partial class GameCreatorWindow : Window
    {
        public GameCreatorWindow()
        {
            InitializeComponent();
            RoundOneQuestionPack.SelectedIndex = 0;
            RoundTwoQuestionPack.SelectedIndex = 0;
        }


        private void Edit_Questions_Pack(object sender, RoutedEventArgs e)
        {
            //var window = new QuestionPacksEditor();
            //if (window.ShowDialog() == true)
            //{
            //    GetQuestionPacks();
            //}
            //else
            //{
            //    GetQuestionPacks();
            //}
        }

        private void GetQuestionPacks()
        {
            var context = DataContext as GameCreatorViewModel;
            if (context != null)
            {
                context.GetQuestionPacks();
                RoundOneQuestionPack.SelectedIndex = 0;
                RoundTwoQuestionPack.SelectedIndex = 0;
            }
        }

        private void On_Window_Closed(object sender, EventArgs e)
        {
            var context = DataContext as GameCreatorViewModel;
            if (context != null && !context.GameStarted)
            {
                Application.Current.Shutdown();
            }
        }

    }
}
