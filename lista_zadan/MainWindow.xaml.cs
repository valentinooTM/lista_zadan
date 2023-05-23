using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

namespace lista_zadan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int numerZadania = 1;
        BindingList<Task> Tasks = new BindingList<Task>();
        BindingList<Task> FilteredTasks = new BindingList<Task>();

        public MainWindow() 
        {
            InitializeComponent();
            taskList.ItemsSource = Tasks;
            filteredTaskList.ItemsSource = FilteredTasks;
            
        }

        private void addTask(object sender, RoutedEventArgs e)
        {
            try
            {
                if (taskName.Text.ToString() != "" && taskCategory.Text.ToString() != "")
                {
                    String nazwaZadania = taskName.Text.ToString();
                    String kategoriaZadania = taskCategory.Text.ToString();

                    DateTime terminZadania = (DateTime)taskDeadline.SelectedDate;

                    TimeSpan tS = terminZadania.Subtract(DateTime.Now);
                    int pozostalyCzas = (int)tS.TotalHours;
                    if (pozostalyCzas > 0)
                    {

                        Tasks.Add(new Task(numerZadania, nazwaZadania, kategoriaZadania, terminZadania, pozostalyCzas));
                        numerZadania++;

                        putCategoryIntoComboBox(sender, e);

                        FilteredTasks.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Podales date wcześniejszą od dzisiaj");
                    }
                }
                else
                {
                    MessageBox.Show("Nie uzupełniłeś formularza");
                }
            }catch (InvalidOperationException nullDate)
            {
                MessageBox.Show("Nie podałeś daty");
            }
        }   


        private void deleteTask(object sender, RoutedEventArgs e)
        {
            Tasks.RemoveAt(taskList.SelectedIndex);
            FilteredTasks.Clear();
        }


        private void putCategoryIntoComboBox(object sender, RoutedEventArgs e)
        {

            List<string> items = new List<string>() {"Wszystkie"};

            foreach (var row in taskList.Items)
            {
                string value = ((Task)row).Category.ToString();

                if(!items.Contains(value)) { items.Add(value); }
                
            }

            categoryBox.ItemsSource = items;

        }


        private void filterTask(object sender, RoutedEventArgs e)
        {
            FilteredTasks.Clear();

            string selectedValue = categoryBox.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedValue))
            {

                foreach (var row in taskList.Items)
                {
                    Task rowTask = row as Task;

                    if (rowTask != null && rowTask.Category == selectedValue)
                    {
                        FilteredTasks.Add(rowTask);
                        
                    }
                }

            }

        }

        public class Task
        {
            public int Number { get;}
            public string Name { get;}
            public string Category { get;}
            public DateTime? Deadline { get;}
            public int TimeLeft { get;}

            public Task(int number, string name, string category, DateTime deadline, int timeleft)
            {
                Number = number;
                Name = name;
                Category = category;
                Deadline = deadline;
                TimeLeft = timeleft;
            }
        }

        
    }
}
