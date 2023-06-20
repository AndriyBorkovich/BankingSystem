using Bankovní_Sýstem.Contexts;
using Bankovní_Sýstem.Views.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for NewAccountWindow.xaml
    /// </summary>
    public partial class NewAccountWindow : Window
    {
        public NewAccountWindow()
        {
            InitializeComponent();
            this.EditAccountButton.IsEnabled = false;
            try
            {
                using var bankingContext = new BankingContext();
                AccountsTable.ItemsSource = bankingContext.Accounts.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var mainMenu = new MainMenuWindow();
            mainMenu.Show();
            this.Close();
        }
        private void ClearInputs()
        {
            var inputValues = new string[] { NameBox.Text, PhoneNumberBox.Text, AdrressBox.Text,
            GenderComboBox.Text, OccupationBox.Text, EducationComboBox.Text, IncomeBox.Text };
            for(int i = 0; i < inputValues.Length; i++)
            {
                inputValues[i] = string.Empty;
            }
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var inputValues = new string[] { NameBox.Text, PhoneNumberBox.Text, AdrressBox.Text,
            GenderComboBox.Text, OccupationBox.Text, EducationComboBox.Text, IncomeBox.Text };
            if (inputValues.Any(string.IsNullOrEmpty))
            {
                MessageBox.Show(this, "Please, fill all fields!", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            else
            {
                using(var bankingContext = new BankingContext())
                {
                    bankingContext.Accounts.Add(new Account()
                    {
                       Name = inputValues[0],
                       Phone = inputValues[1],
                       Address = inputValues[2],
                       Gender = inputValues[3],
                       Occupation = inputValues[4],
                       Education = inputValues[5],
                       Income = Convert.ToDecimal(inputValues[6]),
                       Balance = 0.0m
                    });
                    bankingContext.SaveChanges();
                    AccountsTable.ItemsSource = bankingContext.Accounts.ToList();
                    AccountsTable.Items.Refresh();
                    MessageBox.Show(this, $"Account {inputValues[0]} was added", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearInputs();
                }
            }
        }

        private void EditAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Account selectedAccount = AccountsTable.SelectedItem as Account;
            var inputValues = new string[] { NameBox.Text, PhoneNumberBox.Text, AdrressBox.Text,
            GenderComboBox.Text, OccupationBox.Text, EducationComboBox.Text, IncomeBox.Text };
            if (inputValues.Any(string.IsNullOrEmpty))
            {
                MessageBox.Show(this, "Please, fill all fields!", "Warning!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            else
            {
                if (selectedAccount is not null)
                {
                    Account accountToModify;
                    using (var bankingContext = new BankingContext())
                    {
                        accountToModify = bankingContext.Accounts.FirstOrDefault(x => x.Id == selectedAccount.Id);
                        if (accountToModify != null)
                        {
                            accountToModify.Name = inputValues[0];
                            accountToModify.Phone = inputValues[1];
                            accountToModify.Address = inputValues[2];
                            accountToModify.Gender = inputValues[3];
                            accountToModify.Occupation = inputValues[4];
                            accountToModify.Education = inputValues[5];
                            accountToModify.Income = Convert.ToDecimal(inputValues[6]);
                            bankingContext.Entry(accountToModify).State = EntityState.Modified;
                            bankingContext.SaveChanges();
                            AccountsTable.ItemsSource = bankingContext.Accounts.ToList();
                            AccountsTable.Items.Refresh();
                            MessageBox.Show(this, $"Account {inputValues[0]} was edited", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "Please, select the account first!", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                }
            }
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Account selectedAccount = AccountsTable.SelectedItem as Account, accountToDelete;
            if (selectedAccount is not null)
            {
                using (var bankingContext = new BankingContext())
                {
                    accountToDelete = bankingContext.Accounts.FirstOrDefault(x => x.Id == selectedAccount.Id);
                    if (accountToDelete is not null)
                    {
                        bankingContext.Accounts.Remove(accountToDelete);
                        bankingContext.Entry(accountToDelete).State = EntityState.Deleted;
                        bankingContext.SaveChanges();
                        AccountsTable.ItemsSource = bankingContext.Agents.ToList();
                        AccountsTable.Items.Refresh();
                        MessageBox.Show(this, $"Agent {accountToDelete.Name} was deleted", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearInputs();
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Please, select the account first!", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void AccountsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.EditAccountButton.IsEnabled = true;
            var selectedAccount = AccountsTable.SelectedItem as Account;
            if (selectedAccount is not null)
            {

                NameBox.Text = selectedAccount.Name;
                PhoneNumberBox.Text = selectedAccount.Phone;
                AdrressBox.Text = selectedAccount.Address;
                GenderComboBox.Text = selectedAccount.Gender;
                OccupationBox.Text = selectedAccount.Occupation;
                EducationComboBox.Text = selectedAccount.Education;
                IncomeBox.Text = selectedAccount.Income.ToString();
            }
            else
            {
                Debug.WriteLine("Select student");
            }
        }

        private void EducationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PhoneNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AdrressBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OccupationBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void IncomeBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
