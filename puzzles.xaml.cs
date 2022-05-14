using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для puzzles.xaml
    /// </summary>
    public partial class puzzles : Window
    {
        //22:19 08.05.2022 наконец эта логика ссаная написана и работает вроде как надо...

        string[] allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\puzz\\pics");
        public puzzles()
        {
            InitializeComponent();
            reloadComboBox(false);
        }

        void reloadComboBox(bool saveActive) //если на входе true - с сохранением выбранного элемента (АХТУНГ: У ЭЛЕМЕНТА ОБЯЗАТЕЛЬНО ДОЛЖНО ИЗМЕНИТЬСЯ НАЗВАНИЕ)
        {
            if (saveActive) //если нужно сохранить, то ебёмся (адово бля ебёмся)
            {
                cb1.Items.Clear();
                //----
                string[] backupAllFiles = allfiles;
                string[] backupCuttingPath;
                string backupPicNames = "";
                foreach (string backupFileName in backupAllFiles)
                {
                    backupCuttingPath = backupFileName.Split(new char[] { '\\' });
                    backupPicNames += backupCuttingPath[backupCuttingPath.Length - 1] + "|";
                }
                //----
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\puzz\\pics");
                string[] cutingPath;
                string picNames = "";
                foreach (string fileName in allfiles)
                {
                    cutingPath = fileName.Split(new char[] { '\\' });
                    picNames += cutingPath[cutingPath.Length - 1] + "|";
                }
                //----
                string[] backupedFiles = backupPicNames.Split(new char[] { '|' });
                string[] Files = picNames.Split(new char[] { '|' });
                //----
                int numbNewElement = 0;
                foreach (string file in Files)
                {
                    if (Array.IndexOf(backupedFiles, file) == -1)
                    {
                        numbNewElement = Array.IndexOf(Files, file);
                        break;
                    }                    
                }
                //----
                string[] cutingPathForSave;
                foreach (string fileName in allfiles)
                {
                    cutingPathForSave = fileName.Split(new char[] { '\\' });
                    cb1.Items.Add(cutingPathForSave[cutingPathForSave.Length - 1]);
                }
                cb1.SelectedIndex = numbNewElement;
            }
            else //если не нужно, то нахуя ебаться?
            {
                cb1.Items.Clear();
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\puzz\\pics");
                string[] cutingPath;
                foreach (string fileName in allfiles)
                {
                    cutingPath = fileName.Split(new char[] { '\\' });
                    cb1.Items.Add(cutingPath[cutingPath.Length - 1]);
                }
            }
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Выставляем картинку
                imgBox1.Source = globalVars.NewOpenPic(allfiles[cb1.SelectedIndex]);

                //Выставляем ответ (да я криворукий)
                string[] kek = allfiles[cb1.SelectedIndex].Split(new char[] { '\\' }); //расчленяем путь до картинки
                string lol = kek[kek.Length - 1]; //Получаем имя.расширение картинки
                string[] dotaDva = lol.Split(new char[] { '.' }); //делим на dotaDva[0] = имя | dotaDva[1] = расширение
                string ans = dotaDva[0];
                tb1.Text = ans;
            }
            catch
            {
                //поебать x3 )))
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //"Добавить новый"
        {
            if (cb1.Items.IndexOf("Ответ.png") == -1)
            {
                File.Copy(Directory.GetCurrentDirectory() + "\\Res\\null.png", Directory.GetCurrentDirectory() + "\\puzz\\pics\\Ответ.png", true);
                tb1.Text = "Ответ";
                reloadComboBox(false);
                cb1.SelectedIndex = cb1.Items.IndexOf("Ответ.png");
            }
            else
            {
                MessageBox.Show("Отредактируйте старый перед тем, как добавить новый");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //"Удалить текущий"
        {
            if (cb1.SelectedIndex != -1)
            {
                imgBox1.Source = null;
                File.Delete(Directory.GetCurrentDirectory() + "\\puzz\\pics\\" + cb1.SelectedValue);
                reloadComboBox(false);
            }
            else
            {
                MessageBox.Show("Ребус для удаления не выбран");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //"Выбрать" картинку
        {
            if (cb1.SelectedIndex != -1)
            {
                OpenFileDialog picPicker = new OpenFileDialog();
                picPicker.Title = "Выберете картинку для загрузки";
                picPicker.Filter = "Изображение (PNG, JPG, JPEG) |*.png;*jpg;*.jpeg";
                if ((bool)picPicker.ShowDialog())
                {
                    File.Copy(picPicker.FileName, Directory.GetCurrentDirectory() + "\\puzz\\pics\\" + cb1.SelectedValue, true);
                    string tempMem = cb1.SelectedValue.ToString();
                    reloadComboBox(false);
                    cb1.SelectedIndex = cb1.Items.IndexOf(tempMem);
                }
            }
            else
            {
                MessageBox.Show("Ребус для замены картинки не выбран");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) //"Сохранить"
        {
            if (cb1.SelectedIndex != -1)
            {
                string[] nameAndExpansion = cb1.SelectedValue.ToString().Split(new char[] { '.' });
                File.Move(Directory.GetCurrentDirectory() + "\\puzz\\pics\\" + cb1.SelectedValue, Directory.GetCurrentDirectory() + "\\puzz\\pics\\" + tb1.Text + "." + nameAndExpansion[1]);
                reloadComboBox(true);
            }
            else
            {
                MessageBox.Show("Ребус для сохранения не выбран");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //"На главную"
        {
            admin admPan = new admin(); //СКОВОРОДКА ОДМЕНА))))))))))))))))))))
            admPan.Show();
            this.Close();
        }
    }
}
