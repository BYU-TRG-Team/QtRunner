using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace QtRunner
{
    public class RunSettings : INotifyPropertyChanged
    {
        Configuration Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        string _scriptPath = string.Empty;
        public string ScriptPath {
            get => _scriptPath;
            set {
                if (value == _scriptPath) return;
                _scriptPath = value;
                OnPropertyChanged(nameof(ScriptPath));
            }
        }

        public string PythonPath {
            get => Configuration.AppSettings.Settings["PythonPath"]?.Value ?? string.Empty;
            set {
                if (Configuration.AppSettings.Settings.AllKeys.Contains("PythonPath"))
                {
                    Configuration.AppSettings.Settings["PythonPath"].Value = value;
                } else
                {
                    Configuration.AppSettings.Settings.Add("PythonPath", value);
                }
                Configuration.Save(ConfigurationSaveMode.Modified);
                OnPropertyChanged(nameof(PythonPath));
            }
        }

        string _statusText = string.Empty;
        public string StatusText
        {
            get => _statusText;
            set
            {
                if (value == _statusText) return;
                _statusText = value;
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
