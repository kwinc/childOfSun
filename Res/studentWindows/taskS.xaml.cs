using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    /// Логика взаимодействия для taskS.xaml
    /// </summary>
    public partial class taskS : Window
    {
        string[] allfilesPic = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tasks\\pics");
        string[] allfilesTxt = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tasks");
        string[] tmpFile;
        string[] tmpSplit;
        string[] tmpSplitTwo;
        string[] tmpSplitThree;
        string tmpString;

        public taskS()
        {
            InitializeComponent();
            reloadComboBox();
            //var converter = new BrushConverter();
            //var brushGreen = (Brush)converter.ConvertFromString("#90bcbd");
            //var brushBlack = (Brush)converter.ConvertFromString("#fdc25e");
            //var brushRed = (Brush)converter.ConvertFromString("#b9848a");
        }
        void reloadComboBox()
        {
            cb1.Items.Clear();
            allfilesPic = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tasks\\pics");
            int i = 0;
            foreach (string fileName in allfilesPic)
            {
                i++;
                cb1.Items.Add(i.ToString());
            }
        }
        void loadAns()
        {            
            if (File.Exists(Directory.GetCurrentDirectory() + "\\feedback\\tasks\\" + globalVars.nameG + "_" + globalVars.surnameG + "_" + globalVars.id_usr.ToString() + ".txt"))
            {
                tmpSplitTwo = allfilesPic[cb1.SelectedIndex].Split(new char[] { '\\' }); 
                tmpSplitTwo = tmpSplitTwo[tmpSplitTwo.Length - 1].Split(new char[] { '_' }); //tmpSplitTwo[0] - название задания
                tmpSplitThree = tmpSplitTwo[1].Split(new char[] { '.' });
                tmpFile = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\feedback\\tasks\\" + globalVars.nameG + "_" + globalVars.surnameG + "_" + globalVars.id_usr.ToString() + ".txt");
                foreach (string file in tmpFile)
                {
                    tmpSplit = file.Split(new char[] { '|' });
                    if (tmpSplit[0] == tmpSplitTwo[0])
                    { 
                        tb1.Text = tmpSplit[1];
                        if (tb1.Text == tmpSplitThree[0])
                        {
                            var converter = new BrushConverter();
                            var brushRight = (Brush)converter.ConvertFromString("#6faacf");
                            tb1.Background = brushRight;
                        }
                        else
                        {
                            var converter = new BrushConverter();
                            var brushRed = (Brush)converter.ConvertFromString("#b9848a");
                            tb1.Background = brushRed;
                        }
                        break;                        
                    }
                }                
            }
        }
        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var converter = new BrushConverter();
            var brushWhite = (Brush)converter.ConvertFromString("#FFF");
            tb1.Background = brushWhite;
            tb1.Text = "";
            rtb1.Text = File.ReadAllText(allfilesTxt[cb1.SelectedIndex]);
            imgBox1.Source = globalVars.NewOpenPic(allfilesPic[cb1.SelectedIndex]);
            loadAns();
        }
        private void Button_Click(object sender, RoutedEventArgs e) //"Сохранить"
        {
            string pathToAns = Directory.GetCurrentDirectory() + "\\feedback\\tasks\\" + globalVars.nameG + "_" + globalVars.surnameG + "_" + globalVars.id_usr.ToString() + ".txt";
            string newAnsFile = "";
            if (File.Exists(pathToAns))
            {
                //вычитываем файл с ответами
                tmpFile = File.ReadAllLines(pathToAns);

                //создаем новый файл с ответами
                bool stat = false;
                tmpSplitTwo = allfilesPic[cb1.SelectedIndex].Split('\\');
                tmpSplitTwo = tmpSplitTwo[tmpSplitTwo.Length - 1].Split('_'); //tmpSplitTwo[0] - название выбранного задания
                foreach (string s in tmpFile)
                {
                    tmpSplit = s.Split('|'); //tmpSplit[0] - название задания
                    //MessageBox.Show(tmpSplitTwo[0] + " " + tmpSplit[0]);
                    if (tmpSplit[0] == tmpSplitTwo[0])
                    {
                        //редактируем существующий ответ
                        tmpSplit[1] = tb1.Text;
                        newAnsFile += tmpSplit[0] + "|" + tmpSplit[1] + "\n";
                        stat = true;
                        break;
                    }
                    newAnsFile += s + "\n";
                }

                if (!stat)
                {
                    //добавляяем новый ответ
                    newAnsFile += tmpSplitTwo[0] + "|" + tb1.Text + "\n";
                }

                //MessageBox.Show(newAnsFile);
                File.WriteAllText(pathToAns, newAnsFile);
            }
            else
            {
                tmpSplitTwo = allfilesPic[cb1.SelectedIndex].Split('\\');
                tmpSplitTwo = tmpSplitTwo[tmpSplitTwo.Length - 1].Split('_'); //tmpSplitTwo[0] - название выбранного задания
                newAnsFile = tmpSplitTwo[0] + "|" + tb1.Text;
                File.WriteAllText(pathToAns, newAnsFile);
            }
            loadAns();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            student std = new student();
            std.Show();
            this.Close();
        }
    }
}
