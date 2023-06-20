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
using System.Threading;

namespace Bankovní_Sýstem
{
    /// <summary>
    /// Interaction logic for ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }
        private void DoWork()
        {
            for (int i = 0; i <= 100; i++)
            {
                UpdateProgressBar(i);
                Thread.Sleep(20);
            }
        }
        private void UpdateProgressBar(int value)
        {
            LoadingBar.Value = value;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DoWork();
            if(LoadingBar.Value == 100)
            {
                var login = new Login();
                login.Show();
                this.Close();
            }
        }
    }
}
