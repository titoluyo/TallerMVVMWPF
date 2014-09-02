using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CmdletConsole.Model;

namespace CmdletConsole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICommandLog
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }        

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            this.ExecuteCommand();
        }

        void ICommandLog.WriteLine(string message)
        {
            this.WriteToOutput(message);
        }
       
        private void CommandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.ExecuteCommand();
            }
        }

        //TODO: 04. Main window handles command name & arguments

        private void ExecuteCommand()
        {
            string commandText = this.CommandTextBox.Text;

            if (!string.IsNullOrEmpty(commandText))
            {
                string[] parts = commandText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 0)
                {
                    string commandName = parts[0];

                    string commandArguments = string.Empty;
                    if (commandText.Length > commandName.Length)
                    {
                        commandArguments = commandText.Substring(commandName.Length);
                    }

                    try
                    {
                        CommandProcessor.Current.Execute(commandName, commandArguments);
                    }
                    catch (Exception ex)
                    {
                        this.WriteToOutput(ex.Message);
                    }
                }
            }
        }

        private void WriteToOutput(string message)
        {
            this.OutputTextBox.Text += (message + "\r\n");
        }

        //TODO: 05. Main window implements ICommandLog        

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CommandProcessor.Current.Log = this;
        }


    }
}
