using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testLab
{
    /// <summary>
    /// Логика взаимодействия для PageTest2.xaml
    /// </summary>
    public partial class PageTest2 : Page
    {
        public PageTest2()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            var db = testContext.GetContext();
            var quest = db.Questions.Take(8).ToList();
            quest1.Text = quest[4].Textquestions;
            quest2.Text = quest[5].Textquestions;
            quest3.Text = quest[6].Textquestions;
            quest4.Text = quest[7].Textquestions;
        }
        private void checkResult_Click(object sender, RoutedEventArgs e)
        {
            var db = testContext.GetContext();
            var questions = db.Questions.Take(8).ToList();
            var selectedAnswer1 = GetSelectedAnswer((StackPanel)FindName("answerQuest1"));
            var selectedAnswer2 = GetSelectedAnswer((StackPanel)FindName("answerQuest2"));
            var selectedAnswer3 = GetSelectedAnswer((StackPanel)FindName("answerQuest3"));
            var selectedAnswer4 = GetSelectedAnswer((StackPanel)FindName("answerQuest4"));
            SaveResult(db, questions[4], selectedAnswer1);
            SaveResult(db, questions[5], selectedAnswer2);
            SaveResult(db, questions[6], selectedAnswer3);
            SaveResult(db, questions[7], selectedAnswer4);
            db.SaveChanges();
            NavigationService.Navigate(new PageResult());
        }
        private void SaveResult(testContext db, Question question, string answer)
        {
            var result = new Result
            {
                Idquestions = question.Idquestions,
                Idusers = MainWindow.idUser,
                Answeruser = answer,
                Mark = question.TrueAnswer == answer ? 1 : 0,
                Idtestversion = question.Idtestversion.ToString(),
            };
            db.Results.Add(result);
        }
        private string GetSelectedAnswer(StackPanel stackPanel)
        {
            foreach (var child in stackPanel.Children)
            {
                if (child is RadioButton radio && radio.IsChecked == true)
                    return radio.Content.ToString();
            }
            return null;
        }
    }
}
