using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace Lab2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

          
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, execute_Save, canExecute_Save));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, execute_Open, canExecute_Open));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, execute_Delete, canExecute_Delete));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, execute_Copy, canExecute_Copy));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, execute_Paste, canExecute_Paste));
        }

        // Save
        private void canExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrWhiteSpace(textBox.Text);
        }
        private void execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            File.WriteAllText("Lab2File.txt", textBox.Text);
            MessageBox.Show("Файл збережено!");
        }

        // Open
        private void canExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                var dlg = new OpenFileDialog
                {
                    Filter = "Текстові файли (*.txt)|*.txt|Всі файли (*.*)|*.*"
                };
                if (dlg.ShowDialog() == true)
                    textBox.Text = File.ReadAllText(dlg.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка відкриття файлу: " + ex.Message);
            }
        }

        // Delete
        private void canExecute_Delete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = textBox.Text.Length > 0;
        }
        private void execute_Delete(object sender, ExecutedRoutedEventArgs e)
        {
            if (textBox.SelectionLength > 0)
                textBox.SelectedText = "";
            else
                textBox.Clear();
        }

        // Copy
        private void canExecute_Copy(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = textBox.SelectionLength > 0;
        }
        private void execute_Copy(object sender, ExecutedRoutedEventArgs e)
        {
            textBox.Copy();
        }

        // Paste
        private void canExecute_Paste(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsText();
        }
        private void execute_Paste(object sender, ExecutedRoutedEventArgs e)
        {
            textBox.Paste();
        }

        private void fontSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
   
            if (textBox != null)
                textBox.FontSize = e.NewValue;
        }
    }
}
