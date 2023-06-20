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

namespace Bankovní_Sýstem
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
        }

        private void AccountsLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var accountWindow = new NewAccountWindow();
            accountWindow.Show();
            this.Close();
        }

        private void TransactionsLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var transactionsWindow= new TransactionsWindow();
            transactionsWindow.Show();
            this.Close();
        }

        private void SettingsLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var settingsWindow= new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            this.Close();
        }
    }
}
