using System.Text.RegularExpressions;
namespace PRG_MAUI_Car_Register
{
    class Vehicle
    {
        // Medlemsvariabler
        public enum Type { Bil, MC, Lastbil };
        public const int FirstProductionYear = 1895;
        private Type vehicleType;
        private string registrationNumber = string.Empty;
        private string manufacturer = string.Empty;
        private string model = string.Empty;
        private int year;

        // Konstruktor (en metod med samma namn som klassen, som returnerar ett objekt)
        public Vehicle(Type vehicleType) // en konstruktor kan, men måste inte, ta parametrar
        {
            this.vehicleType = vehicleType;
        }

        // Get-Set för att hålla variablerna privata, och för att validera inkommande värden från UI (user interface, användargränssnittet)
        public string RegistrationNumber
        {
            get { return registrationNumber; }

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ett registreringsnummer måste anges.");

                string text = value.Trim();

                if (text.Length != 6)
                    throw new ArgumentException("Inkorrekt registreringsnummer: det måste bestå av exakt 6 tecken, tre bokstäver följt av två siffror och en siffra eller bokstav.");

                for (int i = 0; i < 3; i++)
                {
                    if (!char.IsLetter(text[i]))
                        throw new ArgumentException("Inkorrekt registreringsnummer: De första tre tecknen måste vara bokstäver.");
                }

                for (int i = 3; i < 3; i++)
                {
                    if (i<5)
                    {
                        if (!char.IsDigit(text[i]))
                            throw new ArgumentException("Inkorrekt registreringsnummer: Det fjärde och femte tecknet måste vara siffror.");
                    }
                    else
                    {
                        if (!char.IsDigit(text[i]) && !char.IsLetter(text[i]))
                            throw new ArgumentException("Inkorrekt registreringsnummer: Det sjätte tecknet måste vara en siffra eller en bokstav.");
                    }
                }
                registrationNumber = value.ToUpper();
            }
        }

        // Fordonstyp tas in från dropdown-menyn, och behöver därför inte valideras
        public Type VehicleType
        {
            get { return vehicleType; }
            set { this.vehicleType = value; }
        }

        //TODO Tillverkare ska valideras, sparas i objektet och visas i UI
        public string Model
        {
            get { return model; }

            set {
                this.model = ValidateText(value, "Modell");
            }
        }

        //TODO Modell ska valideras, sparas i objektet och visas i UI
        public string Manufacturer
        {
            get { return manufacturer; }

            set {
                string text = ValidateText(value, "Tillverkare");

                bool hasLetter = false;
                foreach (char c in text)
                {
                    if (char.IsLetter(c))
                    {
                        hasLetter = true;

                        break;
                    }
                }

                if (!hasLetter)
                    throw new ArgumentException("Tillverkare måste innehålla minst en bokstav.");
                this.manufacturer = text;

            }

            
        }

        //TODO Lägg till möjligheten att spara realistisk årsmodell, validera, spara och visa i objektet och visas i UI. Tips: Regex.IsMatch()
        public int Year
        {
            get { return year; }
            set 
            {
                if (!Regex.IsMatch(value.ToString(), @"^[1-2][0-9][0-9][0-9]$"))
                    throw new ArgumentException("Årsmodell ska anges med fyra siffror i formatet 2024.");

                if (value < FirstProductionYear)
                    throw new ArgumentException($"Den första bilen serietillverkades {FirstProductionYear}. Äldre årsmodeller kan inte registreras.");

                if (value > DateTime.Now.Year)
                    throw new ArgumentException($"Årsmodellen kan inte vara senare än {DateTime.Now.Year}");

                this.year = value;
            }
        }

        public static int ParseYear(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Årsmodell måste anges.");

            string text = input.Trim();

            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                    throw new ArgumentException("Årsmodell får bara innehålla siffror, till exempel 2024.");
            }

            if (!Regex.IsMatch(text, @"^[1-2][0-9][0-9][0-9]$"))
                throw new ArgumentException("^Årsmodell ska skrivas med exakt fyra siffror, till exempel 2024.");

            return int.Parse(text);
        }

        private string ValidateText(string value, string fieldName)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException($"{fieldName} måste anges.");

            string text = value.Trim();

            if (text.Length > 30)
                throw new ArgumentException($"{fieldName} får vara högst 30 tecken.");

            foreach (char c in text)
            {
                if (!char.IsLetter(c) && !char.IsDigit(c) && c != ' ' && c != '-')
                    throw new ArgumentException($"{fieldName} får inte innehålla tecknet '{c}'. Endast bokstäver, siffror, mellanslag och bindestreck är tillåtna.");
            }
            return text;
        }


        //TODO Modifiera overriden på ToString() så att allt visas som önskat i UIs listBox
        public override string ToString()
        {
            return this.registrationNumber + "\t" + this.vehicleType + "\t" +
                   this.manufacturer + "\t" + this.model + "\t" + this.year;
        }
    }
}
