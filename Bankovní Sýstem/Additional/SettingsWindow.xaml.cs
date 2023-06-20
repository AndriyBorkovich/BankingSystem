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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private string _newPassword;
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void SavePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if(NewPasswordBox.Text != "")
            {
                _newPassword = NewPasswordBox.Text;
                MessageBox.Show(this, "New password was set", "Congrats!", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(this, "Please, enter new password first!","Warning",  MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mainMenu = new MainMenuWindow();
            mainMenu.Show();
            Login.Password = _newPassword;
        }
    }
}
