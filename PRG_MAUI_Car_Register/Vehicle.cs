using System.Text.RegularExpressions;

namespace PRG_MAUI_Car_Register
{
    class Vehicle
    {
     
        public enum Type { Bil, MC, Lastbil };
        private Type vehicleType;
        private string registrationNumber = string.Empty;
        private string manufacturer = string.Empty;
        private string model = string.Empty;
        private string yearModel = string.Empty;

        // Konstruktor
        public Vehicle(Type vehicleType)
        {
            this.vehicleType = vehicleType;
        }

        // Registreringsnummer
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

        // Fordonstyp
        public Type VehicleType
        {
            get { return vehicleType; }
            set { this.vehicleType = value; }
        }

        // Modell (måste anges, ej tom)
        public string Model
        {
            get { return model; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Modell får inte vara tom.");
                model = value.Trim();
            }
        }

    
        public string Manufacturer
        {
            get { return manufacturer; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tillverkare får inte vara tom.");
                manufacturer = value.Trim();
            }
        }

   
        public string YearModel
        {
            get { return yearModel; }
            set
            {
                if (!Regex.IsMatch(value, @"^[1-2][0-9]{3}$"))
                    throw new ArgumentException("Årsmodell måste anges i formatet YYYY (t.ex. 2024).");
                yearModel = value;
            }
        }

     
        public override string ToString()
        {
            return $"{registrationNumber}\t{vehicleType}\t{manufacturer}\t{model}\t{yearModel}";
        }
    }
}
