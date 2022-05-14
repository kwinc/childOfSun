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
    /// Логика взаимодействия для task.xaml
    /// </summary>
    public partial class task : Window
    {
        string[] allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tasks\\pics");
        public task()
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
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tasks\\pics");
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
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tasks\\pics");
                string[] cutingPath;
                foreach (string fileName in allfiles)
                {
                    cutingPath = fileName.Split(new char[] { '\\' });
                    cb1.Items.Add(cutingPath[cutingPath.Length - 1]);
                }
            }
        }
        private async void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //подгрузка картинки
                imgBox1.Source = globalVars.NewOpenPic(allfiles[cb1.SelectedIndex]);

                //подгрузка названия
                string[] firstSplit = allfiles[cb1.SelectedIndex].Split(new char[] { '\\' });
                string[] secondSplit = firstSplit[firstSplit.Length - 1].Split(new char[] { '_' });
                tb1.Text = secondSplit[0];

                //подгрузка ответа
                string[] thirdSplit = secondSplit[1].Split(new char[] { '.' });
                tb2.Text = thirdSplit[0];

                //подгрузка текста с заданием
                string[] fourtySplit = firstSplit[firstSplit.Length - 1].Split(new char[] { '.' });
                using (var sr = new StreamReader(Directory.GetCurrentDirectory() + "\\tasks\\" + fourtySplit[0] + ".txt"))
                {
                    rtb1.Text = await sr.ReadToEndAsync();
                }
            }
            catch
            {
                //да-да
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) //"Добавление"
        {
            if (!(File.Exists(Directory.GetCurrentDirectory() + "\\tasks\\pics\\НовоеЗадание_НовыйОтвет.png")) || !(File.Exists(Directory.GetCurrentDirectory() + "\\tasks\\НовоеЗадание_НовыйОтвет.txt")))
            {
                //создание картинки-пустышки
                File.Copy(Directory.GetCurrentDirectory() + "\\Res\\nullForTask.png", Directory.GetCurrentDirectory() + "\\tasks\\pics\\НовоеЗадание_НовыйОтвет.png", true);

                //создание файла-пустышки с текстом задания
                File.WriteAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory() + "\\tasks", "НовоеЗадание_НовыйОтвет.txt"), "Новый текст с заданием");

                //обновление текстбокса (произойдет автоподгрузка всех даннх в формах)
                reloadComboBox(false);
                cb1.SelectedIndex = cb1.Items.IndexOf("НовоеЗадание_НовыйОтвет.png");
            }
            else
            {
                MessageBox.Show("Сначала отредактируйте старый тест");
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) //"Удаление"
        {
            //обнуление данных в форме
            imgBox1.Source = null;
            tb1.Text = "";
            tb2.Text = "";
            rtb1.Text = "";

            //удаление изображения
            File.Delete(Directory.GetCurrentDirectory() + "\\tasks\\pics\\" + cb1.SelectedValue);

            //удаление текстового файла с заданием
            string[] firstSplit = allfiles[cb1.SelectedIndex].Split(new char[] { '\\' });
            string[] secondSplit = firstSplit[firstSplit.Length - 1].Split(new char[] { '.' });            
            File.Delete(Directory.GetCurrentDirectory() + "\\tasks\\" + secondSplit[0] + ".txt");

            //обновление комбобокса
            reloadComboBox(false);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e) //Пикер картинки
        {
            OpenFileDialog picPicker = new OpenFileDialog();
            picPicker.Title = "Выберете картинку для загрузки";
            picPicker.Filter = "Изображение (PNG) |*.png";
            if((bool)picPicker.ShowDialog())
            {
                File.Copy(picPicker.FileName, Directory.GetCurrentDirectory() + "\\tasks\\pics\\" + cb1.SelectedValue, true);
                imgBox1.Source = globalVars.NewOpenPic(Directory.GetCurrentDirectory() + "\\tasks\\pics\\" + cb1.SelectedValue);
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e) //Сохранение
        {
            //проверки
            if (tb1.Text != "" || tb2.Text != "" || rtb1.Text != "")
            {
                //Сохраняем файлы с ответом и названием теста (да, я гениален)
                string[] firstSplit = allfiles[cb1.SelectedIndex].Split(new char[] { '\\' });
                string[] secondSplit = firstSplit[firstSplit.Length - 1].Split(new char[] { '.' });
                File.Delete(Directory.GetCurrentDirectory() + "\\tasks\\" + secondSplit[0] + ".txt"); //удаляем старый файл с текстом задания
                File.WriteAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory() + "\\tasks", tb1.Text + "_" + tb2.Text + ".txt"), rtb1.Text); //сохраняем новый файл с текстом задания

                //переименование файла картинки
                File.Move(Directory.GetCurrentDirectory() + "\\tasks\\pics\\" + secondSplit[0] + ".png", Directory.GetCurrentDirectory() + "\\tasks\\pics\\" + tb1.Text + "_" + tb2.Text + ".png");

                //перезагрузка комбобокса с сохранением выбранного
                reloadComboBox(true);
            }
            else
            {
                MessageBox.Show("Есть пустые поля");
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
