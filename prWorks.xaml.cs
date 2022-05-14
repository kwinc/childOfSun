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
    /// Логика взаимодействия для prWorks.xaml
    /// </summary>
    public partial class prWorks : Window
    {
        string[] allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\prWorks");
        public prWorks()
        {
            InitializeComponent();
            byte kek = 0;
            string[] words;
            foreach (string filename in allfiles)
            {
                kek++;
                words = filename.Split(new char[] { '\\' });
                //cb1.Items.Add(kek + " - " + words[words.Length - 1]);
                cb1.Items.Add(words[words.Length - 1]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберете практическую для загрузки";
            ofd.Filter = "PDF Файл (*.pdf)|*.pdf";
            string[] costil;
            if ((bool)ofd.ShowDialog())
            {
                costil = ofd.FileName.Split(new char[] { '\\' });
                File.Copy(ofd.FileName, Directory.GetCurrentDirectory() + "\\prWorks\\" + costil[costil.Length - 1], true);

                cb1.Items.Clear();
                byte kek = 0;
                string[] words;
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\prWorks");
                foreach (string filename in allfiles)
                {
                    kek++;
                    words = filename.Split(new char[] { '\\' });
                    //cb1.Items.Add(kek + " - " + words[words.Length - 1]);
                    cb1.Items.Add(words[words.Length - 1]);

                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                File.Delete(allfiles[cb1.SelectedIndex]);
                cb1.Items.Clear();
                byte kek = 0;
                string[] words;
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\prWorks");
                foreach (string filename in allfiles)
                {
                    kek++;
                    words = filename.Split(new char[] { '\\' });
                    //cb1.Items.Add(kek + " - " + words[words.Length - 1]);
                    cb1.Items.Add(words[words.Length - 1]);

                }
            }
            catch
            {
                //похуй))
            }
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                pdfPanel.OpenFile(allfiles[cb1.SelectedIndex]);
            }
            catch
            {
                //похуй x2))
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Место для загрузки";
            saveFileDialog1.Filter = "PDF Файл (*.pdf)|*.pdf";
            if ((bool)saveFileDialog1.ShowDialog())
            {
                File.Copy(allfiles[cb1.SelectedIndex], saveFileDialog1.FileName, true);
                //MessageBox.Show(saveFileDialog1.FileName);
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (globalVars.statuss == 3)
            {
                student std = new student();
                std.Show();
                this.Close();
            }
            else if (globalVars.statuss == 1)
            {
                admin adm = new admin();
                adm.Show();
                this.Close();
            }
        }
    }
}
