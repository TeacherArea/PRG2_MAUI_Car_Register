using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_MAUI_Car_Register.Model
{
    public class Truck : Vehicle
    {
        public double LoadCapacity { get; set; }

        public Truck() : base() { }
        public Truck(string registrationNumber, string manufacturer, string model, string yearModel, double loadCapacity)
            : base(registrationNumber, manufacturer, model, yearModel)
        {
            Type = "Truck";

            if (loadCapacity <= 0)
                throw new ArgumentException("Lastkapacitet måste vara större än 0 ton.");
            LoadCapacity = loadCapacity;
        }

        public override string GetDescription() =>
            $"{RegistrationNumber}\tLastbil\t{Manufacturer}\t{Model}\t{YearModel}\t{LoadCapacity} ton";
    }
}