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
    /// Логика взаимодействия для puzzS.xaml
    /// </summary>
    public partial class puzzS : Window
    {
        string[] allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\puzz\\pics");
        int count = 0;
        int record = 0;
        string[] tempSplit;
        bool status = false;
        public puzzS()
        {
            InitializeComponent();
            btnRepeat.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e) //"Далее"
        {
            if (count <= allfiles.Length - 1 && status)
            {
                imgBox1.Source = globalVars.NewOpenPic(allfiles[count]);                
                tempSplit = allfiles[count].Split(new char[] { '\\' });
                tempSplit = tempSplit[tempSplit.Length - 1].Split(new char[] { '.' });
                if (tempSplit[0] == tb1.Text.ToString())
                {
                    record++;
                }
                //MessageBox.Show("Верный:\n" + tempSplit[0] + "\nТвой:\n" + tb1.Text.ToString());
                count++;
                try
                {
                    imgBox1.Source = globalVars.NewOpenPic(allfiles[count]);
                    tb1.Text = "";
                }
                catch
                {
                    btnRepeat.IsEnabled = true;
                    btnNextQ.IsEnabled = false;
                    resl1.Content = "Результат: Верно " + record.ToString() + " из " + allfiles.Length;

                    //создать текстовый файл
                    string[] resultForSave = new string[5];
                    resultForSave[0] = "------------------------";
                    resultForSave[1] = DateTime.Now.ToString();
                    resultForSave[2] = globalVars.nameG + " " + globalVars.surnameG;
                    resultForSave[3] = "Результат: Верно " + record.ToString() + " из " + allfiles.Length;
                    resultForSave[4] = "------------------------\n";
                    DateTime now = DateTime.Now;
                    string dateNow = now.ToString("yyyy-MM-dd");

                    if (File.Exists(Directory.GetCurrentDirectory() + "\\feedback\\puzz\\" + dateNow + ".txt"))
                    {
                        FileStream file = new FileStream(Directory.GetCurrentDirectory() + "\\feedback\\puzz\\" + dateNow + ".txt", FileMode.Append);
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
                        FileStream file = File.Create(Directory.GetCurrentDirectory() + "\\feedback\\puzz\\" + dateNow + ".txt");
                        //new FileStream(Directory.GetCurrentDirectory() + "\\feedback\\puzz\\" + dateNow + ".txt", FileMode.Append);
                        StreamWriter fnew = new StreamWriter(file, Encoding.UTF8);
                        foreach (string text in resultForSave)
                        {
                            fnew.WriteLine(text);
                        }
                        fnew.Close();
                        file.Close();
                    }

                    //File.WriteAllLines(Directory.GetCurrentDirectory() + "\\feedback\\puzz\\" + now.ToString("yyyy-MM-dd hh-mm-ss") + ".txt", resultForSave, Encoding.UTF8);
                }
            }

            if (!status)
            {
                btnNextQ.Content = "Далее";
                imgBox1.Source = globalVars.NewOpenPic(allfiles[count]);                
                status = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //"Начать заново"
        {
            status = false;
            count = 0;
            record = 0;
            btnNextQ.IsEnabled = true;
            btnRepeat.IsEnabled = false;
            resl1.Content = "";
            tb1.Text = "";
            imgBox1.Source = null;
            btnNextQ.Content = "Начать";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            student std = new student();
            std.Show();
            this.Close();
        }
    }
}
