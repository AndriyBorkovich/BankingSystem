using Bankovní_Sýstem.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace Bankovní_Sýstem
{
    /// <summary>
    /// Interaction logic for TransactionsWindow.xaml
    /// </summary>
    public partial class TransactionsWindow : Window
    {
        public TransactionsWindow()
        {
            InitializeComponent();
        }

        private void CheckBalanceButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BalanceTextBox.Text))
            {
                MessageBox.Show(this, "Enter the value!", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            else
            {
                var accountId = Convert.ToInt32(BalanceTextBox.Text);
                using (var bankingContext = new BankingContext())
                {
                    var account = bankingContext.Accounts.FirstOrDefault(x => x.Id == accountId);
                    if (account != null)
                    {
                        BalanceLabel.Content = account.Balance;
                    }
                    else
                    {
                        MessageBox.Show(this, "No account with this id", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void MakeWithdrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountForWithDrawBox.Text == string.Empty || WithdrawAmountBox.Text == string.Empty)
            {
                MessageBox.Show(this, "Enter the values first!", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            else
            {
                var accountId = Convert.ToInt32(AccountForWithDrawBox.Text);
                using (var bankingContext = new BankingContext())
                {
                    var targetAccount = bankingContext.Accounts.FirstOrDefault(x => x.Id == accountId);
                    if (targetAccount != null)
                    {
                        var amountToWithdraw = Convert.ToInt32(WithdrawAmountBox.Text);
                        if (amountToWithdraw > targetAccount.Balance)
                        {
                            MessageBox.Show(this, "Entered amount is too big for this account", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        }
                        else
                        {
                            targetAccount.Balance -= amountToWithdraw;
                            bankingContext.Entry(targetAccount).State = EntityState.Modified;
                            bankingContext.SaveChanges();
                            MessageBox.Show(this, "Operation is successfull", "Congrats", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "No account with this id", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    }
                }
            }

        }

        private void MakeDepositButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountForDepositBox.Text == string.Empty || AmountForDepositBox.Text == string.Empty)
            {
                MessageBox.Show(this, "Enter the values first!", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            else
            {
                var accountId = Convert.ToInt32(AccountForDepositBox.Text);
                using (var bankingContext = new BankingContext())
                {
                    var targetAccount = bankingContext.Accounts.FirstOrDefault(x => x.Id == accountId);
                    if (targetAccount != null)
                    {
                        var amountToWithdraw = Convert.ToInt32(AmountForDepositBox.Text);

                        targetAccount.Balance += amountToWithdraw;
                        bankingContext.Entry(targetAccount).State = EntityState.Modified;
                        bankingContext.SaveChanges();
                        MessageBox.Show(this, "Operation is successfull", "Congrats", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show(this, "No account with this id", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            if (TransferFromBox.Text == string.Empty || AmountToTransfer.Text == string.Empty || TransferToBox.Text == string.Empty)
            {
                MessageBox.Show(this, "Enter the values first!", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            else
            {
                var accountIdFrom = Convert.ToInt32(TransferFromBox.Text);
                var accountIdTo = Convert.ToInt32(TransferToBox.Text);
                using (var bankingContext = new BankingContext())
                {
                    var accountFrom = bankingContext.Accounts.FirstOrDefault(x => x.Id == accountIdFrom);
                    var accountTo = bankingContext.Accounts.FirstOrDefault(x => x.Id == accountIdTo);
                    if (accountFrom != null && accountTo != null)
                    {
                        var amountToTransfer = Convert.ToInt32(AmountToTransfer.Text);
                        if(amountToTransfer > accountFrom.Balance)
                        {
                            MessageBox.Show(this, "Entered amount is too big for this account", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        }
                        else
                        {
                            accountFrom.Balance -= amountToTransfer;
                            accountTo.Balance += amountToTransfer;
                            bankingContext.Entry(accountFrom).State = EntityState.Modified;
                            bankingContext.Entry(accountTo).State = EntityState.Modified;
                            bankingContext.SaveChanges();
                            MessageBox.Show(this, "Operation is successfull", "Congrats", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                        }

                       
                    }
                    else
                    {
                        MessageBox.Show(this, "No accounts with these ids", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
           BalanceLabel.Content = string.Empty;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var mainMenu = new MainMenuWindow();
            mainMenu.Show();
            this.Close();
        }
    }
}
