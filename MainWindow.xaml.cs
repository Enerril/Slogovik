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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FileHelpers;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;


namespace Slogovik
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public TextBlock sup;
        string directoryPath;
        StringBuilder words = new StringBuilder(" ");
        StringBuilder tempWord = new StringBuilder(" ");
        string space = " ";
        //FileHelperEngine<Slog> engine = new FileHelperEngine<Slog>();
        Slog[] result;

        int wN, sN;

        int i, j,tempSN,tempJ;

        string temp;

        Random rn = new Random();

        public MainWindow()
        {
            InitializeComponent();

            ChangeStartTextBlock();
        }

        void ChangeStartTextBlock()
        {
            sup.Text = "This mini-program makes words from random syllables pre-loaded from a csv file. First, open csv file then press MAKE WORDS. Used to create some uniqie logins, nicknames, etc. Each syllable must be on a new line";
            example.Text = "if there are no files with this exe here are two syllable alphabets for rus and eng languages. copy the content with Ctrl+A and save into *.txt file then import into this app. Can use any text input like syllables or letters";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            txtEditor.Text = "";
            words.Clear();
        }

//

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // get csv file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == true)
            {
                var engine = new FileHelperEngine<Slog>(Encoding.UTF8);
                directoryPath = openFileDialog.FileName;
               
                var result = engine.ReadFile(directoryPath);
                /*
                txtEditor.Text = directoryPath;
                */

                /*
                foreach (Slog slog in result)
                {
                    MessageBox.Show(slog.slog);

                }*/
                this.result = result;
            }

            

            if (result != null)
            {
                sylAmount.Text = result.Length.ToString();
            }
            
       



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (result == null)
            {
                MessageBox.Show("no data in the chosen file or file was not chosen");
                return;
            }

            wN = Int32.Parse(wordNumber.Text);
            sN = Int32.Parse(sylNumber.Text);

            if (wN == 0 || wN>100)
            {
                wN = rn.Next(10, 51);
                wordNumber.Text = wN.ToString();
            }
            if (sN == 0|| sN>10)
            {
                sN = rn.Next(2, 11);
                sylNumber.Text = sN.ToString();
            }
            /*
            foreach (Slog slog in result)
            {
                MessageBox.Show(slog.slog);

            }
            */

            // сделать выбор слогов последовательным. можно выбрать позицию 1, 1-2, 1-2-3. 1-3 нельзя и 2-3 нельзя. Для них нужна отдельная функция скорее всего но это уже нафиг.
            // 
            for (i = 0; i < wN; i++)
            {
                tempSN = sN;

                tempJ = 0;

                tempWord.Append(pref.Text);

                if (!String.IsNullOrEmpty(slog1.Text))
                {
                    if (Int32.Parse(slog1.Text) != 0 && Int32.Parse(slog1.Text) <= result.Length)
                    {


                        temp = result[(Int32.Parse(slog1.Text) - 1)].slog;
                        tempJ++;
                        tempWord.Append(temp);
                    }
                }

                if (!String.IsNullOrEmpty(slog2.Text))
                {
                    if (Int32.Parse(slog2.Text) != 0 && Int32.Parse(slog2.Text) <= result.Length)
                    {
                        temp = result[(Int32.Parse(slog2.Text) - 1)].slog;
                        tempJ++;
                        tempWord.Append(temp);
                    }
                }
                if (!String.IsNullOrEmpty(slog3.Text))
                {
                    if (Int32.Parse(slog3.Text) != 0 && Int32.Parse(slog3.Text) <= result.Length)
                    {
                        temp = result[(Int32.Parse(slog3.Text) - 1)].slog;
                        tempJ++;
                        tempWord.Append(temp);
                    }
                }
                   

                for (j = tempJ; j < tempSN; j++)
                {

                    temp = result[rn.Next(0, result.Length)].slog;
                    //MessageBox.Show(temp);
                    //  tempWord.Append(result[rn.Next(0,result.Length)].slog);

                   tempWord.Append(temp);


                }
                //tempWord.Append(space);
                tempWord.Replace(" ", "");
                tempWord.Append(suff.Text);
                tempWord.Append(space);
                words.Append(tempWord);
                tempWord.Clear();


                
            }


            txtEditor.Text = words.ToString();




        }
    }


    // [IgnoreEmptyLines()]
    [DelimitedRecord(",")]
    public class Slog
    {
        //[FieldOptional]
        public string slog;


    }
}
