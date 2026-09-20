using PRG_MAUI_Car_Register.Model;
using System.Collections.ObjectModel;


namespace PRG_MAUI_Car_Register
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Vehicle> vehicleList = new ObservableCollection<Vehicle>();

        public MainPage()
        {
            InitializeComponent();
            pickerType.SelectedIndex = 0;
        }

        private void OnPickerTypeChanged(object sender, EventArgs e)
        {
            string? selectedTypeofvehiclae = pickerType.SelectedItem?.ToString();

            entryDoors.IsVisible = false;
            entryCategory.IsVisible = false;
            entryLoadCapacity.IsVisible = false;      

            switch (selectedTypeofvehiclae){
                case "Bil":
                    entryDoors.IsVisible = true;
                    break;
                case "MC":
                    entryCategory.IsVisible = true;
                    break;
                case "Lastbil":
                    entryLoadCapacity.IsVisible = true;
                    break;
            }
            entryDoors.Text = string.Empty;
            entryCategory.Text = string.Empty;
            entryLoadCapacity.Text = string.Empty;
        }
        private void OnRegisterClicked(object sender, EventArgs e)
        {
            try
            {
                string regNr = entryRegistrationNumber.Text;
                string manufacturer = entryManufacturer.Text;
                string model = entryModel.Text;
                string modelYear = entryModelYear.Text;
                string category = entryCategory.Text;

                Vehicle vehicle;

                string? selectedType = pickerType.SelectedItem?.ToString();

                switch (selectedType)
                {
                    case "Bil":
                        if (!int.TryParse(entryDoors.Text, out int doors))
                        {
                            throw new ArgumentException("Antal dörrar måste vara ett heltal.");
                        }
                        vehicle = new Car(selectedType, regNr, manufacturer, model, modelYear, doors); 
                        break;

                    case "MC":
                        vehicle = new MC(selectedType, regNr, manufacturer, model, modelYear, category); 
                        break;

                    case "Lastbil":
                        if (!double.TryParse(entryLoadCapacity.Text, out double loadCapacity))
                        {
                            throw new ArgumentException("Vilken lastkapacitet i ton har din lastbil.");
                        }
                        vehicle = new Truck(selectedType, regNr, manufacturer, model, modelYear, loadCapacity); 
                        break;

                    default:
                        throw new ArgumentException("Ogiltig fordonstypp");
                }


                vehicleList.Add(vehicle);

                CollectionViewVehicles.ItemsSource = null;
                CollectionViewVehicles.ItemsSource = vehicleList;

                ClearUIFields();

            }
            catch (ArgumentException ex)
            {
                DisplayAlert("Fel", ex.Message, "OK");
            }
        }

        private void OnRadioCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value != true) return;

            IEnumerable<Vehicle> filteredList;

            if (radioCar.IsChecked)
            {
                filteredList = vehicleList.Where(v => v is Car).ToList();
            }
            else if (radioMC.IsChecked)
            {
                filteredList = vehicleList.Where(v => v is MC).ToList();
            }
            else if (radioTruck.IsChecked)
            {
                filteredList = vehicleList.Where(v => v is Truck).ToList();
            }
            else
            {
                filteredList = vehicleList;
            }

            CollectionViewVehicles.ItemsSource = filteredList;
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            string? searchTerm = entrySearchRegistrationNumber.Text?.ToLower();

            if (string.IsNullOrEmpty(searchTerm))
            {
                entrySearchRegistrationNumber.Text = "Ange ett registreringsnummer för att söka.";
                return;
            }

            var foundVehicle = vehicleList.FirstOrDefault(v => v.RegistrationNumber?.ToLower() == searchTerm);

            if (foundVehicle != null)
            {

                string typ = foundVehicle is Car ? "Bil" :
                             foundVehicle is MC ? "MC" :
                             foundVehicle is Truck ? "Lastbil" :
                             "Okänd";


                string AddedInfo = string.Empty;

                if (foundVehicle is Car car)
                {
                    AddedInfo = $"Antal dörrar: {car.Doors}";
                }
                else if (foundVehicle is MC mc)
                {
                    AddedInfo = $"Kategori: {mc.Category}";
                }
                else if (foundVehicle is Truck truck)
                {
                    AddedInfo = $"Lastkapacitet: {truck.LoadCapacity} ton";
                }


                labelSearchResult.Text = $"Fordon hittat:\n" +
                                         $"Registreringsnummer: {foundVehicle.RegistrationNumber}\n" +
                                         $"Tillverkare: {foundVehicle.Manufacturer}\n" +
                                         $"Modell: {foundVehicle.Model}\n" +
                                         $"Årsmodell: {foundVehicle.ModelYear}\n" +
                                         $"Typ: {typ}\n" +
                                         $"{AddedInfo}";

            }
            else
            {
                labelSearchResult.Text = "Inget fordon hittades med det registreringsnumret.";
            }
        }

        private void ClearUIFields()
        {
            entryRegistrationNumber.Text = string.Empty;
            entryManufacturer.Text = string.Empty;
            entryModel.Text = string.Empty;
            entryModelYear.Text = string.Empty;
            entryDoors.Text = string.Empty;
            entryCategory.Text = string.Empty;
            entryLoadCapacity.Text = string.Empty;
        }
    }
}
