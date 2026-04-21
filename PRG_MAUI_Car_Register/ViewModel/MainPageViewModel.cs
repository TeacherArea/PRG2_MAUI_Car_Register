using PRG_MAUI_Car_Register.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using PRG_MAUI_Car_Register.Services;
using System.Diagnostics;

namespace PRG_MAUI_Car_Register.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            Debug.WriteLine("Constructor körs");

            allVehicles = dataService.Load();

            Debug.WriteLine($"Laddade {allVehicles.Count} fordon");

            FilterVehicles();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Action<string, string>? ShowAlert;

        public ObservableCollection<Vehicle> VehicleList { get; set; } = new ObservableCollection<Vehicle>();
        private List<Vehicle> allVehicles = new List<Vehicle>();
        private DataService dataService = new DataService();


        private string registrationNumber = string.Empty;
        public string RegistrationNumber
        {
            get => registrationNumber;
            set { registrationNumber = value; OnPropertyChanged(); }
        }

        private string manufacturer = string.Empty;
        public string Manufacturer
        {
            get => manufacturer;
            set { manufacturer = value; OnPropertyChanged(); }
        }

        private string model = string.Empty;
        public string Model
        {
            get => model;
            set { model = value; OnPropertyChanged(); }
        }

        private string yearModel = string.Empty;
        public string YearModel
        {
            get => yearModel;
            set { yearModel = value; OnPropertyChanged(); }
        }

        private string selectedType = "Bil";
        public string SelectedType
        {
            get => selectedType;
            set { selectedType = value; OnPropertyChanged(); }
        }


        private string selectedCategory = "Alla";
        public string SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged();    
                FilterVehicles();
            }
        }

        private string searchTerm = string.Empty;
        public string SearchTerm
        {
            get => searchTerm;
            set { searchTerm = value; OnPropertyChanged(); }
        }

        private string searchResult = "Sökresultat visas här...";
        public string SearchResult
        {
            get => searchResult;
            set { searchResult = value; OnPropertyChanged(); }
        }


        public void RegisterVehicle()
        {
            try
            {
                Vehicle vehicle = SelectedType switch
                {
                    "Bil" => new Car(RegistrationNumber, Manufacturer, Model, YearModel, 4),
                    "MC" => new Motorcycle(RegistrationNumber, Manufacturer, Model, YearModel, false),
                    "Lastbil" => new Truck(RegistrationNumber, Manufacturer, Model, YearModel, 10),
                    _ => throw new ArgumentException("Välj en giltig fordonstyp."),
                };

                allVehicles.Add(vehicle);

                Debug.WriteLine($"Lägger till fordon: {vehicle.RegistrationNumber}");

                dataService.Save(allVehicles); 

                FilterVehicles();


                RegistrationNumber = string.Empty;
                Manufacturer = string.Empty;
                Model = string.Empty;
                YearModel = string.Empty;
            }
            catch (ArgumentException ex)
            {
                ShowAlert?.Invoke("Fel", ex.Message);
            }
        }

        public void FilterVehicles()
        {
            VehicleList.Clear();

            var filtered = selectedCategory == "Alla"
                ? allVehicles
                : allVehicles.Where(v =>
                    (selectedCategory == "Bil" && v is Car) ||
                    (selectedCategory == "MC" && v is Motorcycle) ||
                    (selectedCategory == "Lastbil" && v is Truck));

            foreach (var v in filtered)
                VehicleList.Add(v);
        }

        public void SearchVehicle()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                ShowAlert?.Invoke("Fel", "Ange ett registreringsnummer för att söka.");
                return;
            }

            var found = allVehicles.FirstOrDefault(v =>
                v.RegistrationNumber?.ToLower() == SearchTerm.ToLower());

            if (found != null)
                SearchResult = $"Fordon hittat:\n{found.Description}";
            else
                SearchResult = "Inget fordon hittades med det registreringsnumret.";
        }
    }
}