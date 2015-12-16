using System.Windows;
using Jeopardy.Model;
using Jeopardy.ViewModel;

namespace Jeopardy.View
{
    /// <summary>
    /// Interaction logic for QuestionPacksEditor.xaml
    /// </summary>
    public partial class QuestionPacksEditor : Window
    {
        public QuestionPacksEditor()
        {
            InitializeComponent();
            QuestionPacks.SelectedIndex = 0;
        }


        private void Create_Question_Click(object sender, RoutedEventArgs e)
        {
            var window = new QuestionWriterWindow();
            var newDataContext = new QuestionsCreatorViewModel();
            if (OpenWindow(window))
            {
                var dataContext = DataContext as QuestionPacksEditorViewModel;
                if (dataContext != null)
                {
                    dataContext.GetQuestionPacks();
                }
            }
        }

        private void Edit_Question_Click(object sender, RoutedEventArgs e)
        {
            var window = new QuestionWriterWindow();
            var newDataContext = window.DataContext as QuestionsCreatorViewModel;
            newDataContext.GameboardToSave = QuestionPacks.SelectedItem as Gameboard;
            if (newDataContext != null)
            {
                int selectedIndex = QuestionPacks.SelectedIndex;
                if (selectedIndex != -1)
                {
                    var dataContext = DataContext as QuestionPacksEditorViewModel;
                    if (dataContext != null)
                    {
                        var questions = dataContext.QuestionPacks[selectedIndex];
                        //newDataContext.GameboardToSave = questions;
                        if (OpenWindow(window))
                        {
                            dataContext.GetQuestionPacks();
                        }
                    }
                }
            }
        }



        private bool OpenWindow(QuestionWriterWindow window)
        {
            if (window.ShowDialog() == true)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
