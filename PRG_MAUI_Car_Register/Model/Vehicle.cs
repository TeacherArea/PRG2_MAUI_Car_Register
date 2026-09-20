using System.Text.RegularExpressions;
namespace PRG_MAUI_Car_Register.Model
{
    abstract class Vehicle
    {
        private string vehicleType = string.Empty;
        private string registrationNumber = string.Empty;
        private string manufacturer = string.Empty;
        private string model = string.Empty;
        private string modelYear = string.Empty;

        public Vehicle(string vehicleType, string registrationNumber, string manufacturer, string model, string modelYear) 
        {
            this.vehicleType = vehicleType;
            RegistrationNumber = registrationNumber;
            Manufacturer = manufacturer;
            Model = model;
            ModelYear = modelYear;
        }
        public string VehicleType { 
            get { return vehicleType; }
            set { vehicleType = value; }
        }
        public string RegistrationNumber
        {
            get { return registrationNumber; }

            set
            {
                if (value.Length == 6)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!char.IsLetter(value[i]))
                            throw new ArgumentException("Inkorret registreringsnummer: De första tre tecknen måste vara bokstäver.");
                    }

                    for (int i = 3; i < 6; i++)
                    {
                        if (i < 5)
                        {
                            if (!char.IsDigit(value[i]))
                                throw new ArgumentException("Inkorret registreringsnummer: Det fjärde och femte tecknet måste vara siffror.");
                        }
                        else
                        {
                            if (!char.IsDigit(value[i]) && !char.IsLetter(value[i]))
                                throw new ArgumentException("Inkorret registreringsnummer: Det sjätte tecknet måste vara en siffra eller en bokstav.");
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Ett registreringsnummer måste bestå av exakt 6 tecken, med tre bokstäver följt av två siffror och en siffra eller bokstav.");
                }

                registrationNumber = value.ToUpper();
            }
        }
        public string Model
        {
            get { return model; }
            set { 

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Du måste skriva in en model för att registrera ditt fordon");
                }
                if (!Regex.IsMatch(value, @"^[a-zA-ZåäöÅÄÖ0-9\s]+$"))
                {
                    throw new ArgumentException("Modellen får endast innehålla bokstäver, siffror, mellanslag.");
                }
                model = value;
            }
        }
        public string Manufacturer
        {
            get { return manufacturer; }
            set { 

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Du måste skriva in tillvärkare för ditt fordon för att registrera det");
                }
                for(int i = 0; i < value.Length; i++)
                {
                    if (!char.IsLetter(value[0]))
                    {
                        throw new ArgumentException("Din tillverkare måste bestå av bokstäver");
                    }
                    
                }
                if (!Regex.IsMatch(value, @"^[a-zA-ZåäöÅÄÖ0-9\s\-]+$"))
                {
                    throw new ArgumentException("Märket får endast innehålla bokstäver, siffror, mellanslag och bindestreck.");
                }
                manufacturer = value;
            }
        }
        public string ModelYear
        {

            get { return modelYear; }
            set { 

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Du måste ange en årsmodell.");
                }

                if (value.Length == 4)
                {
                    for (int i = 0; i < 4; i++){

                        if (!char.IsDigit(value[i])){
                            throw new ArgumentException("Årsmodellen måste bestå av nummer");
                        }
                        else if(i == 0)
                        {
                            if (value[0] == '1' || value[0] == '2') {
                                
                            }
                            else 
                            {
                                throw new ArgumentException("Det första nummret i årsmodellen måste vara en etta eller en tvåa");
                            }
                        }
                    }
                }
                else 
                {
                    throw new ArgumentException("Årsmodellen måste vara 4 nummer");
                }
                int year = int.Parse(value);
                if (year > 1886)
                {

                }
                else
                {
                    throw new ArgumentException("DIN bil kan inte vara yngre än 1886 för att det var då blien uppfanns");
                }
                modelYear = value;
            }
        }
        public abstract string GetDescription();
    }
}
