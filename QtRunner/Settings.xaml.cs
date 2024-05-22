using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace QtRunner
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public string Result { get => pythonPathTextBox.Text; }

        public Settings(string? initialPath = null)
        {
            InitializeComponent();

            if (initialPath != null) pythonPathTextBox.Text = initialPath;
        }

        private void pythonPathBrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var appDataPython = Path.Combine(appData, "Programs", "Python");
            var candidates = Directory.GetDirectories(appDataPython)?.OrderDescending();
            var highestVersion = candidates?.LastOrDefault() ?? string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Python EXE|python.exe"
            };
            if (!string.IsNullOrEmpty(highestVersion))
            {
                openFileDialog.InitialDirectory = highestVersion;
                openFileDialog.Multiselect = false;
            }

            if (openFileDialog.ShowDialog() == true)
            {
                pythonPathTextBox.Text = openFileDialog.FileName;    
            }

        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close(); 
        }
    }
}
