using CF_Utility;
using Microsoft.Extensions.Logging;
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
using System.Windows.Navigation;



namespace CF_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly ICatFactManager _factManager;
        

        public MainWindow()
        {
            ILoggerFactory lf = LoggerFactory.Create(builder => builder.AddConsole());
            _factManager = new CatFactManager(lf);

            InitializeComponent();

            pbStatus.Visibility = Visibility.Hidden;
            tblockFact.Visibility = Visibility.Hidden;

            tbPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CatFacts.txt";
        }

        private async void btGet_Click(object sender, RoutedEventArgs e)
        {


            if (!Directory.Exists(Path.GetDirectoryName(tbPath.Text)))
            {
                MessageBox.Show("Invalid directory.");
                return;
            }
            
          

            tblockFact.Visibility = Visibility.Hidden;
            pbStatus.Visibility= Visibility.Visible;  



            var catFact = await _factManager.GetCatFactAsync();
            await _factManager.SaveCatFactAcync(catFact, tbPath.Text);

            pbStatus.Visibility = Visibility.Hidden;
            tblockFact.Visibility= Visibility.Visible;


            tblockFact.Text = string.Format("Saved fact:\n{0}", catFact.Fact);
           

        }

        private void btFileDlg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text files (*.txt)|*.txt";
            if (fileDialog.ShowDialog() == true)
                tbPath.Text = fileDialog.FileName;
        }
    }
}
