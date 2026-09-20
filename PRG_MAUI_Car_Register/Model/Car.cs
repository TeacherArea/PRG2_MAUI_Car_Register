
namespace PRG_MAUI_Car_Register.Model
{
    internal class Car : Vehicle
    {
        private int doors;
        public Car(string vehicleType, string registrationNumber, string manufacturer, string model, string modelYear, int doors)
    :   base(vehicleType, registrationNumber, manufacturer, model, modelYear)
        {
            Doors = doors;
        }
        public int Doors
        {
            get { return doors; }

            set {
                if (value < 1 || value > 6)
                {
                    throw new ArgumentException("Antal dörrar måste vara mellan 1 och 6.");
                }

                doors = value; }
        }

        public override string GetDescription()
        {
            return $"{base.VehicleType}\t{base.RegistrationNumber}\t{base.Manufacturer}\t{base.Model}\t{base.ModelYear}\t{Doors}";
        }
    }
}
