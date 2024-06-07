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
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Page
    {
        public Autorization()
        {
            InitializeComponent();
            variant.ItemsSource = list;
        }

        private void input_Click(object sender, RoutedEventArgs e)
        {   
            var db = testContext.GetContext();//соединение с БД
            if (name.Text=="" || variant.SelectedItem == null)//проверка на пустоту
            {
                MessageBox.Show("Не все поля заполнены, проверьте данные!!!", "Информация");
            }
            else
            {
                var user = db.Users;
                int idLast = user.OrderByDescending(x => x.Idusers).FirstOrDefault()?.Idusers ?? 0;//нахождение последний индекс 
                var newUser = new User //создание нового пользователя
                {
                    Idusers = idLast + 1,
                    Name = name.Text,
                };
                user.Add(newUser);//добавление
                db.SaveChanges();//сохранение изменений
                MainWindow.idUser = idLast + 1;
                var selectedItem = variant.SelectedItem as string;
                if (selectedItem == "Вариант 1")//проверка на вариант
                {
                    MainWindow.idTestVersion = 1;
                    NavigationService.Navigate(new PageTest());
                }
                else
                {
                    MainWindow.idTestVersion = 2;
                    NavigationService.Navigate(new PageTest2());
                }
            }
        }

        private List<string> list = new List<string>()
        {
            "Вариант 1", "Вариант 2"
        };

        private void variant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
