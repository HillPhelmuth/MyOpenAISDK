using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OpenAITinker.Services
{
    public class AppState : INotifyPropertyChanged
    {
        private string? _appSecret;
        private bool _hasSecret;
        public event PropertyChangedEventHandler? PropertyChanged;

        public string? AppSecret
        {
            get => _appSecret;
            set
            {
                _appSecret = value;
                OnPropertyChanged();
            }
        }

        public bool HasSecret
        {
            get => _hasSecret;
            set
            {
                if (_hasSecret == value) return;
                _hasSecret = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            Console.WriteLine($"Property {propertyName} Changed");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
