﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        public MainWindow()
        {
            InitializeComponent();
            taskList.ItemsSource = Tasks;
            
        }

        private void addTask(object sender, RoutedEventArgs e)
        {
            String nazwaZadania = taskName.Text.ToString(); 
            String kategoriaZadania = taskCategory.Text.ToString();
            DateTime terminZadania = (DateTime)taskDeadline.SelectedDate;

            TimeSpan tS = terminZadania.Subtract(DateTime.Now);
            int pozostalyCzas = (int)tS.TotalHours;

            Tasks.Add(new Task(numerZadania, nazwaZadania, kategoriaZadania, terminZadania, pozostalyCzas));
            numerZadania++;
        }


        private void deleteTask(object sender, RoutedEventArgs e)
        {
            Tasks.RemoveAt(taskList.SelectedIndex);
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
