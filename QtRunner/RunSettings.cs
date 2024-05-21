using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QtRunner
{
    public class RunSettings : INotifyPropertyChanged
    {
        string _scriptPath = string.Empty;
        public string ScriptPath {
            get => _scriptPath;
            set {
                if (value == _scriptPath) return;
                _scriptPath = value;
                OnPropertyChanged(nameof(ScriptPath));
            }
        }

        string _pythonPath = string.Empty;
        public string PythonPath {
            get => _pythonPath;
            set { 
                if (value == _pythonPath) return;
                _pythonPath = value;
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
