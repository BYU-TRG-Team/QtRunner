﻿using System;
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

namespace QtRunner
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        string InitialPath { get; set; }
        public Settings(string initialPath)
        {
            InitializeComponent();

            InitialPath = initialPath;
        }

        private void pythonPathBrowseBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            pythonPathTextBox.Text = InitialPath;
            Close(); 
        }
    }
}