using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;

namespace NakupniSeznam
{
    public class NakupniSeznamViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Polozka> Polozky { get; set; }

        public ICommand PridejPolozkuCommand { get; }
        public ICommand OdeberPolozkuCommand { get; }
        public ICommand UlozSeznamCommand { get; }
        public ICommand NactiSeznamCommand { get; }

        private string _novaPolozkaNazev;
        public string NovaPolozkaNazev
        {
            get => _novaPolozkaNazev;
            set
            {
                _novaPolozkaNazev = value;
                OnPropertyChanged(nameof(NovaPolozkaNazev));
            }
        }

        private int _novaPolozkaMnozstvi;
        public int NovaPolozkaMnozstvi
        {
            get => _novaPolozkaMnozstvi;
            set
            {
                _novaPolozkaMnozstvi = value;
                OnPropertyChanged(nameof(NovaPolozkaMnozstvi));
            }
        }

        public NakupniSeznamViewModel()
        {
            Polozky = new ObservableCollection<Polozka>();

            PridejPolozkuCommand = new RelayCommand(PridejPolozku);
            OdeberPolozkuCommand = new RelayCommand(OdeberPolozku);
            UlozSeznamCommand = new RelayCommand(UlozSeznam);
            NactiSeznamCommand = new RelayCommand(NactiSeznam);
        }

        private void PridejPolozku(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(NovaPolozkaNazev) && NovaPolozkaMnozstvi > 0)
            {
                Polozky.Add(new Polozka(NovaPolozkaNazev, NovaPolozkaMnozstvi));
                NovaPolozkaNazev = string.Empty;
                NovaPolozkaMnozstvi = 0;
            }
        }

        private void OdeberPolozku(object parameter)
        {
            if (parameter is Polozka polozka)
            {
                Polozky.Remove(polozka);
            }
        }

        private void UlozSeznam(object parameter)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Polozky, options);
            File.WriteAllText("nakupniSeznam.json", json);
        }

        private void NactiSeznam(object parameter)
        {
            if (File.Exists("nakupniSeznam.json"))
            {
                string json = File.ReadAllText("nakupniSeznam.json");
                var polozky = JsonSerializer.Deserialize<ObservableCollection<Polozka>>(json);
                Polozky.Clear();
                foreach (var polozka in polozky)
                {
                    Polozky.Add(polozka);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}