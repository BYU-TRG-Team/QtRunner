using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace QtRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RunSettings RunSettings { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = RunSettings;
        }

        private void mainScriptInputBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Filter = "Python files|*.py",
                CheckFileExists = true
            };

            if (openFileDialog.ShowDialog() != true) return;
            RunSettings.ScriptPath = openFileDialog.FileName;
        }

        private void settingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new(RunSettings.PythonPath);
            settings.DataContext = RunSettings;
            if (settings.ShowDialog() != true) return;
        }

        private void runBtn_Click(object sender, RoutedEventArgs e)
        {
            Process process = new();
            process.StartInfo.FileName = string.IsNullOrWhiteSpace(RunSettings.PythonPath) ? "cmd.exe" : RunSettings.PythonPath;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();
            process.StandardInput.WriteLine("python.exe -m venv venv");
            process.StandardInput.WriteLine(@".\venv\Scripts\activate.bat");
            process.StandardInput.WriteLine($"pip install -r {Path.Combine(Path.GetDirectoryName(RunSettings.ScriptPath), "requirements.txt")}");
            process.StandardInput.WriteLine($"python.exe {RunSettings.ScriptPath}");
            process.StandardInput.Flush();
            process.StandardInput.Close();
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            RunSettings.StatusText += e.Data + Environment.NewLine;
        }

        private void statusTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            statusTextBox.ScrollToEnd();
        }
    }
}