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
    /// Логика взаимодействия для vids.xaml
    /// </summary>
    public partial class vids : Window
    {
        string[] allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\videos");
        bool isVidActive = false;
        public vids()
        {
            InitializeComponent();
            reloadComboBox(false, cb1);
        }
        private void HelpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory() + "\\Res\\helpFiles\\help.chm");
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
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\videos");
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
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\videos");
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
            mePlayer.Source = new Uri(Directory.GetCurrentDirectory() + "\\videos\\" + cb1.SelectedValue);
            isVidActive = true;
            mePlayer.Play();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберете видео для загрузки";
            ofd.Filter = "MP4 Файл (*.mp4)|*.mp4";
            string[] costil;
            if ((bool)ofd.ShowDialog())
            {
                costil = ofd.FileName.Split(new char[] { '\\' });
                File.Copy(ofd.FileName, Directory.GetCurrentDirectory() + "\\videos\\" + costil[costil.Length - 1], true);

                reloadComboBox(false, cb1);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.Delete(allfiles[cb1.SelectedIndex]);
                reloadComboBox(false, cb1);
            }
            catch
            {
                //похуй))
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //скачать
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Место для загрузки";
            saveFileDialog1.Filter = "MP4 Файл (*.mp4)|*.mp4";
            if ((bool)saveFileDialog1.ShowDialog())
            {
                File.Copy(allfiles[cb1.SelectedIndex], saveFileDialog1.FileName, true);
                //MessageBox.Show(saveFileDialog1.FileName);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //главная
        {
            if (globalVars.statuss == 3)
            {
                student std = new student();
                std.Show();
                this.Close();
            }
            else if (globalVars.statuss == 1)
            {
                admin admPan = new admin();
                admPan.Show();
                this.Close();
            }
        }

        /*        private void Button_Click_2(object sender, RoutedEventArgs e) //пауза
                {
                    if (isVidActive)
                    {
                        player1.Pause();
                        isVidActive = false;
                    }

                    if (!isVidActive)
                    {
                        player1.Play();
                        isVidActive = true;
                    }

                }*/
        void timer_Tick(object sender, EventArgs e)
        {
            if (mePlayer.Source != null)
            {
                if (mePlayer.NaturalDuration.HasTimeSpan)
                    lblStatus.Content = String.Format("{0} / {1}", mePlayer.Position.ToString(@"mm\:ss"), mePlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
                lblStatus.Content = "No file selected...";
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Stop();
        }
    }
}
