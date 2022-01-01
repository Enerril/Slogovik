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

        int i, j,tempSN;

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
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            txtEditor.Text = "";
        }

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
                result = engine.ReadFile(directoryPath);
                /*
                txtEditor.Text = directoryPath;
                */
                
                /*
                foreach (Slog slog in result)
                {
                    MessageBox.Show(slog.slog);

                }*/
                
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
            for (i = 0; i < wN; i++)
            {
                tempSN = sN;

                for (j = 0; j < tempSN; j++)
                {

                    temp = result[rn.Next(0, result.Length)].slog;
                    //MessageBox.Show(temp);
                    //  tempWord.Append(result[rn.Next(0,result.Length)].slog);

                   tempWord.Append(temp);


                }
                //tempWord.Append(space);
                tempWord.Replace(" ", "");
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
