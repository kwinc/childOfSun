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
using System.Windows.Shapes;

namespace V4._0
{
    /// <summary>
    /// Логика взаимодействия для student.xaml
    /// </summary>
    public partial class student : Window
    {
        public student()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //лекции
        {
            lectures lct = new lectures();
            lct.btAdd.IsEnabled = false;
            lct.btRemove.IsEnabled = false;
            lct.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //практические
        {
            prWorks prw = new prWorks();
            prw.btnAdd.IsEnabled = false;
            prw.btnRemove.IsEnabled = false;
            prw.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //презентации
        {
            pres prs = new pres();
            prs.btAdd.IsEnabled = false;
            prs.btRemove.IsEnabled = false;
            prs.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //ребусы
        {
            Res.studentWindows.puzzS puzzS = new Res.studentWindows.puzzS();
            puzzS.Show();
            this.Close();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e) //выход
        {
            globalVars.statuss = 0;
            MainWindow mn = new MainWindow();
            mn.Show();
            this.Close();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e) //тесты
        {
            Res.studentWindows.testS testS = new Res.studentWindows.testS();
            testS.Show();
            this.Close();
        }
        private void Button_Click_6(object sender, RoutedEventArgs e) //задания
        {
            Res.studentWindows.taskS taskS = new Res.studentWindows.taskS();
            taskS.Show();
            this.Close();
        }
        private void Button_Click_7(object sender, RoutedEventArgs e) //кроссворды
        {
            Res.studentWindows.crosswordS crw = new Res.studentWindows.crosswordS();
            crw.Show();
            this.Close();
        }
        private void Button_Click_8(object sender, RoutedEventArgs e) //видео
        {
            vids vid = new vids();
            vid.btnAdd.IsEnabled = false;
            vid.btnRemove.IsEnabled = false;
            vid.Show();
            this.Close();
        }
    }
}
