using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Jeopardy.Model;
using Jeopardy.ViewModel;

namespace Jeopardy.View
{
    /// <summary>
    /// Interaction logic for QuestionPacksEditor.xaml
    /// </summary>
    public partial class QuestionPacksEditor : UserControl
    {
        public WrapperWindow WrapperWindow { get; private set; }

        public QuestionPacksEditor(WrapperWindow wrapperWindow)
        {
            WrapperWindow = wrapperWindow;

            InitializeComponent();

            QuestionPacks.SelectionChanged += QuestionPacks_SelectionChanged;
            CategoriesList.SelectionChanged += CategoriesList_SelectionChanged;

            QuestionPacks.SelectedIndex = -1;
            QuestionPacks.SelectedIndex = 0;

            this.Loaded += QuestionPacksEditor_Loaded;
        }

        void QuestionPacksEditor_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (TextBox tb in FindVisualChildren<TextBox>(this))
            {
                tb.GotFocus += On_Textbox_Got_Focus;
            }
        }

        void On_Textbox_Got_Focus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (IsDefaultText(textBox.Text))
                {
                    textBox.Text = "";
                }
            }
        }

        private bool IsDefaultText(string p)
        {
            for (int i = 1; i <= 6; i++)
            {
                if (string.Equals(string.Format(Category.CATEGORY_NAME, i), p, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (string.Equals(string.Format(Category.QUESTION_FORMAT, i), p, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (string.Equals(string.Format(Category.ANSWER_FORMAT, i), p, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        void CategoriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = DataContext as QuestionPacksEditorViewModel;
            if (context != null)
            {
                context.CurrentCategory = (Category)CategoriesList.SelectedItem;
            }
        }

        void QuestionPacks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = DataContext as QuestionPacksEditorViewModel;
            if (context != null)
            {
                context.CurrentQuestionPack = (Gameboard)QuestionPacks.SelectedItem;
                CategoriesList.SelectedIndex = -1;
                CategoriesList.SelectedIndex = 0;
            }
        }

        private void DeleteQuestionPack(object sender, RoutedEventArgs e)
        {
            if (QuestionPacks.SelectedItem != null)
            {
                var context = DataContext as QuestionPacksEditorViewModel;
                if (context != null)
                {
                    context.DeleteGameboardsCommand.Execute(QuestionPacks.SelectedItem);
                    if (QuestionPacks.SelectedIndex == -1)
                    {
                        QuestionPacks.SelectedIndex = 0;
                    }
                }
            }
        }

        private void SaveAllQuestionsPack(object sender, RoutedEventArgs e)
        {
            var context = DataContext as QuestionPacksEditorViewModel;
            if (context != null)
            {
                context.SaveGameboardsCommand.Execute(null);
            }
            WrapperWindow.ShowStartupWindow();
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

    }
}
