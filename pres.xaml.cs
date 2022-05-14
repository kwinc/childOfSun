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
using System.Windows.Xps.Packaging;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Win32;

namespace V4._0
{
    /// <summary>
    /// Логика взаимодействия для pres.xaml
    /// </summary>
    public partial class pres : Window
    {
        private static XpsDocument ConvertPowerPointToXps(string pptFilename, string xpsFilename)
        {
            var pptApp = new Microsoft.Office.Interop.PowerPoint.Application();

            var presentation = pptApp.Presentations.Open(pptFilename, MsoTriState.msoTrue, MsoTriState.msoFalse,
                MsoTriState.msoFalse);

            try
            {
                presentation.ExportAsFixedFormat(xpsFilename, PpFixedFormatType.ppFixedFormatTypeXPS);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to export to XPS format: " + ex);
            }
            finally
            {
                presentation.Close();
                pptApp.Quit();
            }

            return new XpsDocument(xpsFilename, FileAccess.Read);
        }

        string[] allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\pres");
        public pres()
        {
            InitializeComponent();
            byte kek = 0;
            string[] words;
            foreach (string filename in allfiles)
            {
                kek++;
                words = filename.Split(new char[] { '\\' });
                cb1.Items.Add(kek + " - " + words[words.Length - 1]);
            }
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string powerPointFile = allfiles[cb1.SelectedIndex]; // allfiles[cb1.SelectedIndex] - это путь к файлу .pptx
                var xpsFile = System.IO.Path.GetTempPath() + Guid.NewGuid() + ".xps";
                var xpsDocument = ConvertPowerPointToXps(powerPointFile, xpsFile);
                DocumentviewPowerPoint.Document = xpsDocument.GetFixedDocumentSequence();
            }
            catch
            {
                //похуй x2))
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //добавить
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберете презентацию для загрузки";
            ofd.Filter = "PPTX Файл (*.pptx)|*.pptx";
            string[] costil;
            if ((bool)ofd.ShowDialog())
            {
                costil = ofd.FileName.Split(new char[] { '\\' });
                File.Copy(ofd.FileName, Directory.GetCurrentDirectory() + "\\pres\\" + costil[costil.Length - 1], true);

                cb1.Items.Clear();
                byte kek = 0;
                string[] words;
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\pres");
                foreach (string filename in allfiles)
                {
                    kek++;
                    words = filename.Split(new char[] { '\\' });
                    cb1.Items.Add(kek + " - " + words[words.Length - 1]);
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //удалить
        {
            try
            {
                File.Delete(allfiles[cb1.SelectedIndex]);
                cb1.Items.Clear();
                byte kek = 0;
                string[] words;
                allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\pres");
                foreach (string filename in allfiles)
                {
                    kek++;
                    words = filename.Split(new char[] { '\\' });
                    cb1.Items.Add(kek + " - " + words[words.Length - 1]);
                }
            }
            catch
            {
                //похуй))
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) //скачать
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Место для загрузки";
            saveFileDialog1.Filter = "PPTX Файл (*.pptx)|*.pptx";
            if ((bool)saveFileDialog1.ShowDialog())
            {
                File.Copy(allfiles[cb1.SelectedIndex], saveFileDialog1.FileName, true);
                //MessageBox.Show(saveFileDialog1.FileName);
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (globalVars.statuss == 1)
            {
                admin adm = new admin();
                adm.Show();
                this.Close();
            }
            else if (globalVars.statuss == 3)
            {
                student student = new student();
                student.Show();
                this.Close();
            }
            
        }
    }
}
