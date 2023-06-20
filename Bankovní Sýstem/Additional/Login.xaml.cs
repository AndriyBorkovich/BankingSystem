using Bankovní_Sýstem.Contexts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly string _login;
        public static string Password;
        public Login()
        {
            InitializeComponent();
            _login = "Andrew25";
            Password = "qwerty123";
        }

        

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(UserNameBox.Text) || string.IsNullOrEmpty(UserPasswordBox.Password.ToString()))
            {
                // переписати
                MessageBox.Show(this, "Fields are empty");
            }
            else
            {
                var userName = UserNameBox.Text;
                var password = UserPasswordBox.Password.ToString();
                if (RoleComboBox.Text == "Admin" && userName == _login && password == Password)
                {
                    var agentsWindow = new AgentsWindow();
                    agentsWindow.Show();
                    this.Close();
                }
                else if (RoleComboBox.Text == "Agent")
                {
                   using(var bankingContext = new BankingContext())
                   {
                        var user = bankingContext.Agents.FirstOrDefault(x => x.UserName == userName && x.Password == password);
                        if (user is not null)
                        {
                            var mainMenuWindow = new MainMenuWindow();
                            mainMenuWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "Invalid login or password!", "Ooops...", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        }
                   }
                  
                }
                else
                {
                    MessageBox.Show(this, "Invalid login or password!", "Ooops...", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                }

            }
        }
    }
}
