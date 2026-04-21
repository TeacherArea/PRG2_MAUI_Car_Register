using System.Text.RegularExpressions;

namespace PRG_MAUI_Car_Register.Model
{
    public abstract class Vehicle
    {
        private string registrationNumber = string.Empty;
        private string manufacturer = string.Empty;
        private string model = string.Empty;
        private string yearModel = string.Empty;
        public string Type { get; set; }

        protected Vehicle() { }
        protected Vehicle(string registrationNumber, string manufacturer, string model, string yearModel)
        {
            RegistrationNumber = registrationNumber;
            Manufacturer = manufacturer;
            Model = model;
            YearModel = yearModel;
        }

        public string RegistrationNumber
        {
            get => registrationNumber;
            set
            {
                if (value.Length == 6)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!char.IsLetter(value[i]))
                            throw new ArgumentException("Fel: De första tre tecknen måste vara bokstäver.");
                    }

                    for (int i = 3; i < 6; i++)
                    {
                        if (i < 5)
                        {
                            if (!char.IsDigit(value[i]))
                                throw new ArgumentException("Fel: Det fjärde och femte tecknet måste vara siffror.");
                        }
                        else
                        {
                            if (!char.IsDigit(value[i]) && !char.IsLetter(value[i]))
                                throw new ArgumentException("Fel: Det sjätte tecknet måste vara en siffra eller bokstav.");
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Registreringsnumret måste bestå av exakt 6 tecken.");
                }

                registrationNumber = value.ToUpper();
            }
        }

        public string Manufacturer
        {
            get => manufacturer;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tillverkare får inte vara tom.");
                manufacturer = value.Trim();
            }
        }

        public string Model
        {
            get => model;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Modell får inte vara tom.");
                model = value.Trim();
            }
        }

        public string YearModel
        {
            get => yearModel;
            set
            {
                if (!Regex.IsMatch(value, @"^[1-2][0-9]{3}$"))
                    throw new ArgumentException("Årsmodell måste anges i formatet YYYY (t.ex. 2024).");
                yearModel = value;
            }
        }

    
        public abstract string GetDescription();
        public string Description => GetDescription();
        public override string ToString() => GetDescription();
    }
}