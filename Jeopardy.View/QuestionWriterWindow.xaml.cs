using System.Windows;
using System.Windows.Controls;
using Jeopardy.ViewModel;

namespace Jeopardy.View
{
    /// <summary>
    /// Interaction logic for QuestionWriterWindow.xaml
    /// </summary>
    public partial class QuestionWriterWindow : UserControl
    {
        public QuestionWriterWindow()
        {
            //DataContext = new QuestionsCreatorViewModel();
            InitializeComponent();
        }

        private void Save_Question_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = DataContext as QuestionsCreatorViewModel;
            dataContext.SaveQuestionsCommand.Execute(null);
            //this.Close();
        }
    }
}
