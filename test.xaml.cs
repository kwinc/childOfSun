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
    /// Логика взаимодействия для test.xaml
    /// </summary>
    public partial class test : Window
    {
        public test()
        {
            InitializeComponent();
            reloadComboBox(false, cb1);
        }
        /*
         * Структура файла с тестом:
         * НазваниеТеста.txt
         *      НомерВопроса[int]|ТекстВопроса[string]|ВариантОтвета1[string]|ВариантОтвета2[string]|ВариантОтвета3[string]|НомерВерногоВариантаОтвета[int]
         *      НомерВопроса[int]|ТекстВопроса[string]|ВариантОтвета1[string]|ВариантОтвета2[string]|ВариантОтвета3[string]|НомерВерногоВариантаОтвета[int]
         * 
         * Думаю понятно, что новый вопрос = новая строка
         * бля для кого я это пишу?..
         * 
         */

        string[] allfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tests");
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
        void loadTestData(string[] questData, bool onlyNumbs) //метод для безМозгоЁбного подгрузки вопроса, если true - подгрузка только комбобокс2
        {
            if (!onlyNumbs)
            { 
                //подгрузка номера вопроса
                cb2.SelectedIndex = cb2.Items.IndexOf(questData[0]);

                //подгрузка текста вопроса
                rtb1.Text = questData[1];

                //подгрузка вариантов ответа
                tba1.Text = questData[2];
                tba2.Text = questData[3];
                tba3.Text = questData[4];

                //постановка "точки" на верный вариант ответа
                switch (Convert.ToInt32(questData[5]))
                {
                    case 1: rba1.IsChecked = true; break;
                    case 2: rba2.IsChecked = true; break;
                    case 3: rba3.IsChecked = true; break;
                    default: MessageBox.Show("Ты чё мне скормить собрался?"); break;
                }
            }
            else
            {
                cb2.Items.Add(questData[0]);
            }
        }
        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //очистка дерьма
                cb2.Items.Clear();

                //подгрузка названия
                string[] firstSplit = allfiles[cb1.SelectedIndex].Split(new char[] { '\\' }); //получаем названиеТеста.txt в firstSplit[firstSplit.Length - 1]
                string[] secondSplit = firstSplit[firstSplit.Length - 1].Split(new char[] { '.' }); //разделяем firstSplit[firstSplit.Length - 1] на название и txt
                tb1.Text = secondSplit[0];

                //подгрузка вопросов                
                string question;
                string[] questionData;
                for (byte i = 0; i <= File.ReadAllLines(allfiles[cb1.SelectedIndex]).Length - 1; i++)
                {
                    question = File.ReadLines(allfiles[cb1.SelectedIndex]).Skip(i).First();
                    questionData = question.Split(new char[] { '|' });
                    loadTestData(questionData, true);                    
                }
                cb2.SelectedIndex = 0;

            }
            catch
            {
                //пустотааааааааа в головееееееееееее
            }
        }
        private void cb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //читаем нужную строку
            string question = File.ReadLines(allfiles[cb1.SelectedIndex]).Skip(Convert.ToInt32(cb2.SelectedValue) - 1).First();

            //расчленяем ее
            string[] questionData = question.Split(new char[] { '|' });

            //загружаем сам тест
            loadTestData(questionData, false); 
        }
        private void Button_Click(object sender, RoutedEventArgs e) //"Добавить новый"
        {
            if (!(File.Exists(Directory.GetCurrentDirectory() + "\\tests\\НовыйТест.txt")))
            {
                //создание файла-пустышки для последующего редактирования
                File.WriteAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory() + "\\tests", "НовыйТест.txt"), "1|ТекстВопроса1|ВариантОтвета11|ВариантОтвета21|ВариантОтвета31|1");

                //подгрузка новоиспечённого дерьма
                reloadComboBox(false, cb1);
                cb1.SelectedIndex = cb1.Items.IndexOf("НовыйТест.txt");
            }
            else
            {
                MessageBox.Show("Чтобы добавить новый тест отредактируйте старый");
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) //"Удалить текущий"
        {
            //удаление фала с тестом
            File.Delete(Directory.GetCurrentDirectory() + "\\tests\\" + cb1.SelectedValue);

            //обнуление сроков путина
            tb1.Text = "";
            rtb1.Text = "";
            tba1.Text = "";
            tba2.Text = "";
            tba3.Text = "";

            //обновление комбобокса1
            reloadComboBox(false, cb1);            
        }
        private void Button_Click_3(object sender, RoutedEventArgs e) //"Добавить вопрос"
        {
            //получение номера нового вопроса
            string numbForNextQ = Convert.ToString(cb2.Items.Count + 1);

            //формирование строки с новым вопросом
            string question  = numbForNextQ + "|ТекстВопроса" + numbForNextQ + "|ВариантОтвета1" + numbForNextQ + "|ВариантОтвета2" + numbForNextQ + "|ВариантОтвета3" + numbForNextQ + "|1";

            //string[] question  = new string[1] { numbForNextQ + "|ТекстВопроса" + numbForNextQ + "|ВариантОтвета1" + numbForNextQ + "|ВариантОтвета2" + numbForNextQ + "|ВариантОтвета3" + numbForNextQ + "|1" };

            //добавление строки конец файла с тестом
            //File.WriteAllLines(Directory.GetCurrentDirectory() + "\\tests\\" + cb1.SelectedValue, question, Encoding.UTF8);
            using (var fileTest = new StreamWriter(Directory.GetCurrentDirectory() + "\\tests\\" + cb1.SelectedValue, true))
            {
                //Добавляем к старому содержимому файла
                fileTest.Write("\n" + question);
                fileTest.Close();
            }

            //костыльная перезагрузка комбобокс2
            loadTestData(question.Split(new char[] {'|'}), true);
            cb2.SelectedIndex = cb2.Items.IndexOf(numbForNextQ);
        }
        private void Button_Click_4(object sender, RoutedEventArgs e) //"Удаалить текущий вопрос"
        {
            //10.05.2022 11:44 - запомните меня жизнерадостным дединсайдиком, ибо сейчас начнётся такой секс...

            //сначала удаляем строку с таким вопросом и парралельно идёт перерасчёт номеров вопросов в файле
            string[] fileTest = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\tests\\" + cb1.SelectedValue, Encoding.UTF8);
            string[] newFileTest = new string[fileTest.Length - 1];
            string[] tempSplit;
            int questionNumber = 1;
            foreach (string s in fileTest)
            {
                tempSplit = s.Split(new char[] { '|' });
                if (!(cb2.SelectedValue.ToString() == tempSplit[0]))
                {
                    newFileTest[questionNumber - 1] = questionNumber.ToString() + "|" + tempSplit[1] + "|" + tempSplit[2] + "|" + tempSplit[3] + "|" + tempSplit[4] + "|" + tempSplit[5];
                    questionNumber++;
                }
                else
                {
                    //MessageBox.Show(questionNumber.ToString());                    
                    break;
                }
            }
            for (int i = questionNumber; i <= newFileTest.Length; i++)
            {               
                tempSplit = fileTest[i].Split(new char[] { '|' });
                newFileTest[i - 1] = questionNumber.ToString() + "|" + tempSplit[1] + "|" + tempSplit[2] + "|" + tempSplit[3] + "|" + tempSplit[4] + "|" + tempSplit[5];
                questionNumber++;
            }

            //и в самом конце записываем результат
            string AllItemsInOneString = string.Join("\n", newFileTest);
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\tests\\" + cb1.SelectedValue, AllItemsInOneString, Encoding.UTF8);


            //не забываем обновить ебучий комбобокс
            cb2.Items.Clear();
            foreach (string temp in newFileTest)
            {
                tempSplit = temp.Split(new char[] { '|' });                
                cb2.Items.Add(tempSplit[0]);
            }

            //10.05.2022 18:13 вау на это ушло меньше суток...
        }
        private void Button_Click_5(object sender, RoutedEventArgs e) //"Сохранить"
        {
            //вычитываем файл с тестом
            string[] fileTest = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\tests\\" + cb1.SelectedValue, Encoding.UTF8);

           //служебные переменные
            string[] newFileTest = new string[fileTest.Length];
            string questionForEdit = cb2.SelectedValue.ToString();
            string[] tempSplit;

            //запускаем дерьмо
            for (int i = 0; i <= fileTest.Length - 1; i++)
            {
                tempSplit = fileTest[i].Split(new char[] { '|' });
                if (questionForEdit != tempSplit[0])
                {
                    newFileTest[i] = (i + 1).ToString() + "|" + tempSplit[1] + "|" + tempSplit[2] + "|" + tempSplit[3] + "|" + tempSplit[4] + "|" + tempSplit[5];
                }
                else
                {
                    if (rba1.IsChecked == true)
                    {
                        tempSplit[5] = "1";
                    }
                    else if (rba2.IsChecked == true)
                    {
                        tempSplit[5] = "2";
                    }
                    else
                    {
                        tempSplit[5] = "3";
                    }
                    newFileTest[i] = (i + 1).ToString() + "|" + rtb1.Text.ToString() + "|" + tba1.Text.ToString() + "|" + tba2.Text.ToString() + "|" + tba3.Text.ToString() + "|" + tempSplit[5].ToString();
                }
            }

            //записываем результат дерьма          
            //File.WriteAllLines(Directory.GetCurrentDirectory() + "\\tests\\" + cb1.SelectedValue, newFileTest, Encoding.UTF8);            
            string AllItemsInOneString = string.Join("\n", newFileTest);
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\tests\\" + cb1.SelectedValue, AllItemsInOneString, Encoding.UTF8);

            //сохраняем название теста и обновляем комбобокс с сохранением
            File.Move(Directory.GetCurrentDirectory() + "\\tests\\" + cb1.SelectedValue, Directory.GetCurrentDirectory() + "\\tests\\" + tb1.Text + ".txt");
            reloadComboBox(true, cb1);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //"На главную"
        {
            admin admPan = new admin(); //СКОВОРОДКА ОДМЕНА (мне все еще смешно)
            admPan.Show();
            this.Close();
        }        
    }
}
