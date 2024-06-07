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
    /// Логика взаимодействия для PageResult.xaml
    /// </summary>
    public partial class PageResult : Page
    {
        public PageResult()
        {
            InitializeComponent();
            Load();
        }
        private void Load()//загрузка результатов из БД
        {
            var db = testContext.GetContext();
            var myList = from a in db.Results
                         join b in db.Questions on a.Idquestions equals b.Idquestions
                         where a.Idusers == MainWindow.idUser && a.Idtestversion == MainWindow.idTestVersion.ToString()//выборка по варианту и индефикатору пользователя
                         select new
                         {
                             Вопрос = b.Textquestions,
                             Правильный = b.TrueAnswer,
                             Пользователь = a.Answeruser,
                             Оценка = a.Mark
                         };
            datagrid.ItemsSource = myList.ToList();//Вывод в таблицу
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());//реализация перехода
        }
    }
}
