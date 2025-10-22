using PRG_MAUI_Car_Register.Model;

namespace PRG_MAUI_Car_Register
{
    public partial class MainPage : ContentPage
    {
        List<Vehicle> vehicleList = new List<Vehicle>();

        public MainPage()
        {
            InitializeComponent();
            pickerType.SelectedIndex = 0;
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            try
            {
                string regNr = entryRegistrationNumber.Text;
                string manufacturer = entryManufacturer.Text;
                string model = entryModel.Text;
                string yearModel = entryYearModel.Text;

                Vehicle vehicle = null;


                string selectedType = pickerType.SelectedItem?.ToString();

                if (selectedType == "Bil")
                {
                    vehicle = new Car(regNr, manufacturer, model, yearModel, 4);
                }
                else if (selectedType == "MC")
                {
                    vehicle = new Motorcycle(regNr, manufacturer, model, yearModel, false);
                }
                else if (selectedType == "Lastbil")
                {
                    vehicle = new Truck(regNr, manufacturer, model, yearModel, 10);
                }
                else
                {
                    throw new ArgumentException("Välj en giltig fordonstyp.");
                }

                vehicleList.Add(vehicle);


                listViewVehicles.ItemsSource = null;
                listViewVehicles.ItemsSource = vehicleList;


                entryRegistrationNumber.Text = string.Empty;
                entryManufacturer.Text = string.Empty;
                entryModel.Text = string.Empty;
                entryYearModel.Text = string.Empty;
            }
            catch (ArgumentException ex)
            {
                DisplayAlert("Fel", ex.Message, "OK");
            }
        }

        private void OnRadioCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value != true) return;

            List<Vehicle> filteredList;

            if (radioCar.IsChecked)
            {
               
                filteredList = vehicleList.Where(v => v is Car).ToList();
            }
            else if (radioMC.IsChecked)
            {
             
                filteredList = vehicleList.Where(v => v is Motorcycle).ToList();
            }
            else if (radioTruck.IsChecked)
            {
               
                filteredList = vehicleList.Where(v => v is Truck).ToList();
            }
            else
            {
            
                filteredList = vehicleList;
            }

            listViewVehicles.ItemsSource = filteredList;
        }
        private void OnSearchClicked(object sender, EventArgs e)
        {
            string searchTerm = entrySearchRegistrationNumber.Text?.ToLower();

            if (string.IsNullOrEmpty(searchTerm))
            {
                entrySearchRegistrationNumber.Text = "Ange ett registreringsnummer för att söka.";
                return;
            }

            var foundVehicle = vehicleList.FirstOrDefault(v => v.RegistrationNumber?.ToLower() == searchTerm);

            if (foundVehicle != null)
            {
                labelSearchResult.Text = $"Fordon hittat:\n{foundVehicle.GetDescription()}";
            }
            else
            {
                labelSearchResult.Text = "Inget fordon hittades med det registreringsnumret.";
            }
        }

    }
}
