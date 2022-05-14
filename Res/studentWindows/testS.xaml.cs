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

namespace V4._0.Res.studentWindows
{
    /// <summary>
    /// Логика взаимодействия для testS.xaml
    /// </summary>
    public partial class testS : Window
    {
        string[] allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tests");
        string[] activeTest;
        string[] activeQuestion;
        bool status = false;
        int record = 0;
        int count = 0;
        string activeAnsRight;
        string activeAns;
        public testS()
        {
            InitializeComponent();
            reloadComboBox(false, cb1);
        }

        void reloadComboBox(bool saveActive, ComboBox cb) //если на входе true - с сохранением выбранного элемента (АХТУНГ: У ЭЛЕМЕНТА ОБЯЗАТЕЛЬНО ДОЛЖНО ИЗМЕНИТЬСЯ НАЗВАНИЕ)
        {
            if (saveActive) //если нужно сохранить, то ебёмся (адово бля ебёмся)
            {
                cb.Items.Clear();
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
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tests");
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
                    cb.Items.Add(cutingPathForSave[cutingPathForSave.Length - 1]);
                }
                cb.SelectedIndex = numbNewElement;
            }
            else //если не нужно, то нахуя ебаться?
            {
                cb.Items.Clear();
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tests");
                string[] cutingPath;
                foreach (string fileName in allfiles)
                {
                    cutingPath = fileName.Split(new char[] { '\\' });
                    cb.Items.Add(cutingPath[cutingPath.Length - 1]);
                }
            }
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbres1.Content.ToString() != "")
            {
                record = 0;
                count = 0;
                rba1.IsChecked = false;
                rba2.IsChecked = false;
                rba3.IsChecked = false;
                lbres1.Content = "";
                MessageBox.Show("Выполнилось");
            }

            activeTest = File.ReadAllLines(allfiles[cb1.SelectedIndex], Encoding.UTF8);
            cb1.IsEnabled = false;
            status = true;

            activeQuestion = activeTest[count].Split(new char[] { '|' });
            rtbQuestion.Text = activeQuestion[1];
            rba1.Content = activeQuestion[2];
            rba2.Content = activeQuestion[3];
            rba3.Content = activeQuestion[4];           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            student std = new student();
            std.Show();
            this.Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            //получение верного ответа
            switch (Convert.ToInt32(activeQuestion[5]))
            {
                case 1: activeAnsRight = "1"; break;
                case 2: activeAnsRight = "2"; break;
                case 3: activeAnsRight = "3"; break;
            }

            //получение ответа поьзователя
            if (rba1.IsChecked == true)
            {
                activeAns = "1";
            }
            else if (rba2.IsChecked == true)
            {
                activeAns = "2";
            }
            else if (rba3.IsChecked == true)
            {
                activeAns = "3";
            }
            else
            {
                activeAns = "0";
            }

            //проверка ответа
            if (activeAns == activeAnsRight)
            {
                record++;
            }

            //загрузка следующего вопроса
            count++;
            try
            {
                activeQuestion = activeTest[count].Split(new char[] { '|' });
                rtbQuestion.Text = activeQuestion[1];
                rba1.Content = activeQuestion[2];
                rba2.Content = activeQuestion[3];
                rba3.Content = activeQuestion[4];
                rba1.IsChecked = false;
                rba2.IsChecked = false;
                rba3.IsChecked = false;
            }
            catch
            {
                cb1.IsEnabled = true;
                //MessageBox.Show(record.ToString());
                lbres1.Content = "Результаты:\nНабранно баллов: " + record.ToString() + " из " + activeTest.Length;

                //запись результата в файл
                string[] resultForSave = new string[5];
                resultForSave[0] = "------------------------";
                resultForSave[1] = DateTime.Now.ToString();
                resultForSave[2] = globalVars.nameG + " " + globalVars.surnameG;
                resultForSave[3] = "Результат: Верно " + record.ToString() + " из " + activeTest.Length;
                resultForSave[4] = "------------------------\n";
                DateTime now = DateTime.Now;
                string dateNow = now.ToString("yyyy-MM-dd");

                if (File.Exists(Directory.GetCurrentDirectory() + "\\feedback\\tests\\" + dateNow + ".txt"))
                {
                    FileStream file = new FileStream(Directory.GetCurrentDirectory() + "\\feedback\\tests\\" + dateNow + ".txt", FileMode.Append);
                    StreamWriter fnew = new StreamWriter(file, Encoding.UTF8);
                    foreach (string text in resultForSave)
                    {
                        fnew.WriteLine(text);
                    }
                    fnew.Close();
                    file.Close();
                }
                else
                {
                    FileStream file = File.Create(Directory.GetCurrentDirectory() + "\\feedback\\tests\\" + dateNow + ".txt");
                    //new FileStream(Directory.GetCurrentDirectory() + "\\feedback\\puzz\\" + dateNow + ".txt", FileMode.Append);
                    StreamWriter fnew = new StreamWriter(file, Encoding.UTF8);
                    foreach (string text in resultForSave)
                    {
                        fnew.WriteLine(text);
                    }
                    fnew.Close();
                    file.Close();
                }
            }


        }
    }
}
