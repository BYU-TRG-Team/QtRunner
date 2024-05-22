using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Filter = "Python files|*.py",
                CheckFileExists = true
            };

            if (openFileDialog.ShowDialog() != true) return;
            RunSettings.ScriptPath = openFileDialog.FileName;
        }

        private void settingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new(RunSettings.PythonPath);
            if (settings.ShowDialog() == true) RunSettings.PythonPath = settings.Result;
        }

        private void runBtn_Click(object sender, RoutedEventArgs e)
        {
            Process process = new();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();

            var pythonExe = File.Exists(RunSettings.PythonPath) ? RunSettings.PythonPath : "python.exe";
            var scriptDirectory = Path.GetDirectoryName(RunSettings.ScriptPath);
            var requirementsTxt = Path.Combine(scriptDirectory!, "requirements.txt");
            process.StandardInput.WriteLine($"{pythonExe} -m venv venv");
            process.StandardInput.WriteLine(@".\venv\Scripts\activate.bat");
            if (File.Exists(requirementsTxt))
            {
                process.StandardInput.WriteLine($"pip install -r {requirementsTxt}");
            }
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

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}