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
using System.Data;

namespace V4._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        dataBaseConnect dbc = new dataBaseConnect();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(textBox_login.Text == ""))
            {
                if (!(textBox_password.Password == ""))
                {
                    DataTable dt_user = dbc.Select("SELECT * FROM [dbo].[users] WHERE [login] = '" + textBox_login.Text + "' AND [password] = '" + textBox_password.Password + "'");

                    if (dt_user.Rows.Count != 0)
                    {
                        string name = dt_user.Rows[0].Field<string>("name");
                        string surname = dt_user.Rows[0].Field<string>("surname");
                        int status = dt_user.Rows[0].Field<int>("id_status");
                        int id_user = dt_user.Rows[0].Field<int>("id_user");

                        globalVars.nameG = name;
                        globalVars.surnameG = surname;
                        globalVars.statuss = status;
                        globalVars.id_usr = id_user;
                        if (status == 3)
                        {
                            student sdt = new student();
                            sdt.Show();
                            this.Close();
                        }
                        else if (status == 2)
                        {

                        }
                        else
                        {
                            admin adm = new admin();
                            adm.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя нет");
                        textBox_login.Text = "";
                        textBox_password.Password = "";
                    }
                }
                else
                {
                    MessageBox.Show("Пароль где?");
                }
            }
            else
            {
                MessageBox.Show("Логин где?");
            }
        }
    }
}
