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

namespace V4._0
{
    /// <summary>
    /// Логика взаимодействия для crosswrd.xaml
    /// </summary>
    public partial class crosswrd : Window
    {
        string[] allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\crosswords");
        Dictionary<string, TextBox> dict = new Dictionary<string, TextBox>();
        string[,] ansCross = new string[20, 20];

        string[] tempSplit;
        public crosswrd()
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
            try
            {
                var converter = new BrushConverter();
                var brushBlack = (Brush)converter.ConvertFromString("#ffc15e");
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
            catch
            {
                //дикий костыль
            }
        }
        void recNewFileCros(string path, string[,] array)
        {
            File.WriteAllText(path,
              string.Concat(array.Cast<string>().Select(
                (s, i) => s + ((i + 1) % array.GetLength(1) == 0 ? "\n" : "")
              ))
            );
        }
        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadCrossword(Directory.GetCurrentDirectory() + "\\crosswords\\" + cb1.SelectedValue, true);
            if (File.Exists(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue))
            {
                tb1.Document.Blocks.Clear();
                tb1.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue))));
                //tb1.Text = File.ReadAllText(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue);
            }
            else
            {
                try
                {
                    File.Copy(Directory.GetCurrentDirectory() + "\\Res\\crossQ.txt", Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue, true);
                    tb1.Document.Blocks.Clear();
                    tb1.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue))));
                    //tb1.Text = File.ReadAllText(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue);
                }
                catch
                {

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //На главную
        {
            admin admPan = new admin();
            admPan.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //Добавить новый
        {
            //генерируем файл пустышку
            int tempJ = 0;
            string[,] newCros = new string[20,20];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    newCros[i, j] = "*|";
                    tempJ = j;
                }
                newCros[i, tempJ] += "*";
            }

            if (cb1.Items.Count == 0)
            {
                //File.Create(Directory.GetCurrentDirectory() + "\\crosswords\\Кроссворд1.txt");
                recNewFileCros(Directory.GetCurrentDirectory() + "\\crosswords\\Кроссворд1.txt", newCros);
                reloadComboBox(false, cb1);
                cb1.SelectedIndex = cb1.Items.IndexOf("Кроссворд1.txt");
            }
            else if (cb1.Items.Count != 10)
            {
                tempSplit = allfiles[allfiles.Length - 1].Split(new char[] { '\\' });
                tempSplit = tempSplit[tempSplit.Length - 1].Split(new char[] { '.' });
                tempSplit = tempSplit[0].Split(new char[] { 'д' });
                string newName = Convert.ToString(Convert.ToInt32(tempSplit[1]) + 1);
                //File.Create(Directory.GetCurrentDirectory() + "\\crosswords\\Кроссворд" + newName + ".txt");
                recNewFileCros(Directory.GetCurrentDirectory() + "\\crosswords\\Кроссворд" + newName + ".txt", newCros);
                reloadComboBox(false, cb1);
                cb1.SelectedIndex = cb1.Items.IndexOf("Кроссворд" + newName + ".txt");

            }
            else
            {
                MessageBox.Show("Достигнуто максимальное число кроссвордов");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //Удалить текущий
        {
            if (cb1.SelectedIndex != -1)
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\crosswords\\" + cb1.SelectedValue);
                File.Delete(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue);
                reloadComboBox(false, cb1);
                loadCrossword(Directory.GetCurrentDirectory() + "\\Res\\cross.txt", true);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //Сохранить
        {
            if (cb1.SelectedIndex != -1)
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue, new TextRange(tb1.Document.ContentStart, tb1.Document.ContentEnd).Text);

                //File.WriteAllText(Directory.GetCurrentDirectory() + "\\crosswordsQuestions\\" + cb1.SelectedValue, tb1.Text);

                int count = 0;
                int tempJJ = 0;
                string[,] tempFile = new string[20, 20];
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        tempFile[i, j] = dict["Block" + count.ToString()].Text + "|";
                        count++;
                        tempJJ = j;
                    }
                    tempFile[i, tempJJ] = dict["Block" + (count - 1).ToString()].Text;
                }

                /*                for (int i = 0; i < 20; i++)
                                {
                                    for (int j = 0; j < 20; j++)
                                    {

                                    }
                                }*/

                recNewFileCros(Directory.GetCurrentDirectory() + "\\crosswords\\" + cb1.SelectedValue, tempFile);
            }
            else
            {
                MessageBox.Show("Не выбран кроссворд");
            }
        }
    }
}
