
namespace PRG_MAUI_Car_Register.Model
{
    internal class MC : Vehicle
    {
        public string category = string.Empty;
        public MC(string vehicleType, string registrationNumber, string manufacturer, string model, string modelYear, string category)
    :    base(vehicleType, registrationNumber, manufacturer, model, modelYear)
        {
            Category = category;
        }

        public string Category
        {
            get { return category; }

            set { category = value;}
        }
        public override string GetDescription()
        {
            return $"{base.VehicleType}\t{base.RegistrationNumber}\t{base.Manufacturer}\t{base.Model}\t{base.ModelYear}\t{category}";
        }
    }
}
