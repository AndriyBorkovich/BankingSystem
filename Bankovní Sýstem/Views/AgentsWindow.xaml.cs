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
    /// Interaction logic for AgentsWindow.xaml
    /// </summary>
    public partial class AgentsWindow : Window
    {
        public AgentsWindow()
        {
            InitializeComponent();
            try
            {
                using var bankingContext = new BankingContext();
                AgentsTable.ItemsSource = bankingContext.Agents.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }

        private void ClearInputs()
        {
            var inputValues = new string[] { AgentNameBox.Text, AgentPasswordBox.Text, AgentAdrressBox.Text, AgentPhoneBox.Text };
            for(int i = 0; i < inputValues.Length; i++)
            {
                inputValues[i] = string.Empty;
            }
        }

        // додавання агента до БД
         
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var inputValues = new string[] { AgentNameBox.Text, AgentPasswordBox.Text, AgentAdrressBox.Text, AgentPhoneBox.Text };
            if(inputValues.Any(string.IsNullOrEmpty))
            {
                MessageBox.Show(this, "Please, fill all fields!", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            else 
            {
                using (var bankingContext = new BankingContext())
                {
                    bankingContext.Agents.Add(new Agent()
                    {
                        UserName = inputValues[0],
                        Password = inputValues[1],
                        Address = inputValues[2],
                        Phone = inputValues[3]
                    });

                    bankingContext.SaveChanges();
                    AgentsTable.ItemsSource = bankingContext.Agents.ToList();
                    AgentsTable.Items.Refresh();
                    MessageBox.Show(this, $"Agent {inputValues[0]} was added", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearInputs();
                }
            }
            
        }

        // редагування агента у БД

        private void EditAgentButton_Click(object sender, RoutedEventArgs e)
        {
            Agent selectedAgent = AgentsTable.SelectedItem as Agent;
            var inputValues = new string[] { AgentNameBox.Text, AgentPasswordBox.Text, AgentAdrressBox.Text, AgentPhoneBox.Text };
            if(inputValues.Any(string.IsNullOrEmpty))
            {
                MessageBox.Show(this, "Please, fill all fields!", "Warning!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            else
            {
                if (selectedAgent is not null)
                {
                    Agent agentToModify;
                    using (var bankingContext = new BankingContext())
                    {
                        agentToModify = bankingContext.Agents.FirstOrDefault(x => x.Id == selectedAgent.Id);
                        if (agentToModify != null)
                        {
                            agentToModify.UserName = inputValues[0];
                            agentToModify.Password = inputValues[1];
                            agentToModify.Address = inputValues[2];
                            agentToModify.Phone = inputValues[3];
                            bankingContext.Entry(agentToModify).State = EntityState.Modified;
                            bankingContext.SaveChanges();
                            AgentsTable.ItemsSource = bankingContext.Agents.ToList();
                            AgentsTable.Items.Refresh();
                            MessageBox.Show(this, $"Agent {inputValues[0]} was edited", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "Please, select the agent first!", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                }
            }
           
           
        }

        // видалення агента з БД
        private void DeleteAgentButton_Click(object sender, RoutedEventArgs e)
        {
            Agent selectedAgent = AgentsTable.SelectedItem as Agent, agentToDelete;
            if (selectedAgent is not null)
            {
                using (var bankingContext = new BankingContext())
                {
                    agentToDelete = bankingContext.Agents.FirstOrDefault(x => x.Id == selectedAgent.Id);
                    if (agentToDelete is not null)
                    {
                        bankingContext.Agents.Remove(agentToDelete);
                        bankingContext.Entry(agentToDelete).State = EntityState.Deleted;
                        bankingContext.SaveChanges();
                        AgentsTable.ItemsSource = bankingContext.Agents.ToList();
                        AgentsTable.Items.Refresh();
                        MessageBox.Show(this, $"Agent { agentToDelete.UserName } was deleted", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearInputs();
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Please, select the agent first!", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }

        }

        private void AgentsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAgent = AgentsTable.SelectedItem as Agent;
            if (selectedAgent is not null)
            {

                AgentNameBox.Text = selectedAgent.UserName;
                AgentPasswordBox.Text = selectedAgent.Password;
                AgentAdrressBox.Text = selectedAgent.Address;
                AgentPhoneBox.Text = selectedAgent.Phone;
            }
            else
            {
                Debug.WriteLine("Select agent");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var mainMenu = new MainMenuWindow();
            mainMenu.Show();
            this.Close();
        }
    }
}
