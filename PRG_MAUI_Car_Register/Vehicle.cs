namespace PRG_MAUI_Car_Register
{
    class Vehicle
    {
        // Medlemsvariabler
        public enum Type { Bil, MC, Lastbil };
        private Type vehicleType;
        private string registrationNumber = string.Empty;
        private string manufacturer = string.Empty;
        private string model = string.Empty;
        private string yearmodel = string.Empty;

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
        public string Yearmodel
        {
            get { return yearmodel; } 
            set
            {
 
                if (string.IsNullOrWhiteSpace(value)){
                    throw new ArgumentException("Årsmodellen kan ej vara tom, detta fält måste fyllas i");
                }
                if (value.Any(char.IsLetter))
                {
                    throw new ArgumentException("Årsmodellen kan bara innehålla siffror");
                }
                if (value.Length >= 2)
                {
                    string firstTwo = value.Substring(0, 2);
                    if (firstTwo != "18" && firstTwo != "19" && firstTwo != "20")
                    {
                        throw new ArgumentException("Årsmodellen måste vara från ett giltigt år, 1800, 1900 eller 2000-talet");
                    }
                }

                if(value.Length != 2 && value.Length != 4)
                {
                    throw new ArgumentException("Årsmodellen ska skrivas med 2 eller 4 siffror. Ex: 1989 eller 89");
                }

                    this.yearmodel = value;
            }
        }

        // Fordonstyp tas in från dropdown-menyn, och behöver därför inte valideras
        public string Manufacturer
        {
            get { return manufacturer; }
            set {

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Tillverkarnamnet kan ej vara tomt, detta fält måste fyllas i");

                }

                    
                if (value.Any(char.IsDigit)){
                    throw new ArgumentException("Tillverkarnamnet kan ej innehålla siffror");
                }
                string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
                foreach (var item in specialChar)
                {
                    if (value.Contains(item))
                    {
                        throw new ArgumentException("Tillverkarnamnet kan ej innehålla specialtecken");
                    }

                }
            


                this.manufacturer = value.ToUpper();
            }
        }
        public Type VehicleType
        {
            get { return vehicleType; }
            set { this.vehicleType = value; }
        }


        public string Model
        {
            get { return model; }
            set {
                if(string.IsNullOrWhiteSpace(value))
                {
                  
                  throw new ArgumentException("Modelnamnet kan ej vara tomt, detta fält måste fyllas i");

                }
                string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
                foreach (var item in specialChar)
                {
                    if (value.Contains(item))
                    {
                        throw new ArgumentException("Modelnamnet" +
                            " kan ej innehålla specialtecken");
                    }

                }

                this.model = value; }
        }

      





        // Klassens  eventuella övriga metoder brukar finnas här, här en override av ToString()

        //TODO Modifiera overriden på ToString() så att allt visas som önskat i UIs listBox
        public override string ToString()
        {
            return this.registrationNumber + "\t" + this.vehicleType + "\t" + this.manufacturer + "\t" + this.model;
        }
    }
}
