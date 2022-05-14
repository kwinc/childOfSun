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
using System.IO;

namespace V4._0.Res.studentWindows
{
    /// <summary>
    /// Логика взаимодействия для crosswordS.xaml
    /// </summary>
    public partial class crosswordS : Window
    {
        string[] allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\crosswords");
        Dictionary<string, TextBox> dict = new Dictionary<string, TextBox>();
        string[,] ansCross = new string[20, 20];
        string[] tempSplit;
        public crosswordS()
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
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\crosswords");
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
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\crosswords");
                string[] cutingPath;
                foreach (string fileName in allfiles)
                {
                    cutingPath = fileName.Split(new char[] { '\\' });
                    cb.Items.Add(cutingPath[cutingPath.Length - 1]);
                }
            }
        }
        void loadCrossword(string pathToTxt, bool loadAns)
        {
            var converter = new BrushConverter();
            var brushBlack = (Brush)converter.ConvertFromString("#fdc25e");
            var brushRed = (Brush)converter.ConvertFromString("#b9848a");
            string[] fileAsCross = System.IO.File.ReadAllLines(pathToTxt);
            string[] tempSplit;

            for (int i = 0; i < grid1.RowDefinitions.Count * grid1.ColumnDefinitions.Count; i++)
            {
                dict["Block" + i.ToString()] = new TextBox();
                dict["Block" + i.ToString()].TextAlignment = TextAlignment.Center;
                dict["Block" + i.ToString()].HorizontalContentAlignment = HorizontalAlignment.Center;
                dict["Block" + i.ToString()].VerticalAlignment = VerticalAlignment.Center;
                if (loadAns)
                {
                    dict["Block" + i.ToString()].MaxLength = 2;
                }
                else
                {
                    dict["Block" + i.ToString()].MaxLength = 1;
                }
                dict["Block" + i.ToString()].FontSize = 18;
                //dict["Block" + i.ToString()].Text = "X";
                V6._0.WatermarkService.SetWatermark(dict["Block" + i.ToString()], "");
            }

            int count = 0;
            for (int i = 0; i < grid1.RowDefinitions.Count; i++)
            {
                tempSplit = fileAsCross[i].Split(new char[] { '|' });
                for (int j = 0; j < grid1.ColumnDefinitions.Count; j++)
                {
                    if (tempSplit[j] != "*" || loadAns)
                    {
                        //dict["Block" + count.ToString()].Text = tempSplit[j];

                        Grid.SetRow(dict["Block" + count.ToString()], i);
                        Grid.SetColumn(dict["Block" + count.ToString()], j);
                        grid1.Children.Add(dict["Block" + count.ToString()]);
                        count++;
                    }
                    else
                    {

                        dict["Block" + count.ToString()].Background = brushBlack;

                        Grid.SetRow(dict["Block" + count.ToString()], i);
                        Grid.SetColumn(dict["Block" + count.ToString()], j);
                        grid1.Children.Add(dict["Block" + count.ToString()]);
                        count++;
                    }

                }
            }

            string[] tempSplitTwo;
            int govnoChlens = 0;
            char tempChar;
            for (int i = 0; i < fileAsCross.Length; i++)
            {
                tempSplitTwo = fileAsCross[i].Split(new char[] { '|' });
                for (int j = 0; j < tempSplitTwo.Length; j++)
                {
                    if (loadAns)
                    {
                        ansCross[i, j] = tempSplitTwo[j];
                        dict["Block" + govnoChlens.ToString()].Text = ansCross[i, j];
                        govnoChlens++;
                    }
                    else
                    {
                        tempChar = tempSplitTwo[j][0];
                        if (Char.IsDigit(tempChar))
                        {
                            ansCross[i, j] = tempSplitTwo[j][1].ToString();
                            V6._0.WatermarkService.SetWatermark(dict["Block" + govnoChlens.ToString()], tempChar);
                            govnoChlens++;
                        }
                        else
                        {
                            ansCross[i, j] = tempSplitTwo[j];
                            govnoChlens++;
                        }
                    }

                }
            }
        }
        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadCrossword(Directory.GetCurrentDirectory() + "\\crosswords\\" + cb1.SelectedValue, false);
            if (File.Exists(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue))
            {
                //lab1.Document.Blocks.Clear();
                //lab1.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue))));
                //lab1.IsEnabled = false;
                lab1.Text = File.ReadAllText(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue);
            }
            else
            {
                //File.Copy(Directory.GetCurrentDirectory() + "\\Res\\crossQ.txt", Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue, true);
                //tb1.Document.Blocks.Clear();
                //tb1.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue))));
                MessageBox.Show("Кроссворд повреждён");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) //на главную
        {
            student std = new student();
            std.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //проверить
        {
            string[] fileAsCross = System.IO.File.ReadAllLines(System.IO.Directory.GetCurrentDirectory() + "\\crosswords\\" + cb1.SelectedValue);
            //string[,] ansCross = new string[17, 17];
            var converter = new BrushConverter();
            var brushGreen = (Brush)converter.ConvertFromString("#90bcbd");
            var brushRed = (Brush)converter.ConvertFromString("#b9848a");
            int countLol = 0;
            for (int i = 0; i < grid1.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < grid1.ColumnDefinitions.Count; j++)
                {
                    if (dict["Block" + countLol.ToString()].Text == ansCross[i, j])
                    {
                        dict["Block" + countLol.ToString()].Background = brushGreen;
                        countLol++;
                    }
                    else if (ansCross[i, j] == "*")
                    {
                        //MessageBox.Show("Что в текстбоксе:\n" + dict["Block" + countLol.ToString()].Text + "\n---------------\nЧто должно быть:\n" + ansCross[i, j]);
                        //dict["Block" + countLol.ToString()].Background = brushBlack;
                        countLol++;
                    }
                    else
                    {
                        dict["Block" + countLol.ToString()].Background = brushRed;
                        countLol++;
                    }
                }
            }
        }

        
    }
}
