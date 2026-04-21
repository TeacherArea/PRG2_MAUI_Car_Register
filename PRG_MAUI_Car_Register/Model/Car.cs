using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_MAUI_Car_Register.Model
{
    public class Car : Vehicle
    {
        public int Doors { get; set; }
        public Car() : base() { }

        public Car(string registrationNumber, string manufacturer, string model, string yearModel, int doors)
            : base(registrationNumber, manufacturer, model, yearModel)
        {
            Type = "Car";

            if (doors <= 0)
                throw new ArgumentException("En bil måste ha minst en dörr.");
            Doors = doors;
        }

        public override string GetDescription() =>
            $"{RegistrationNumber}\tBil\t{Manufacturer}\t{Model}\t{YearModel}\t{Doors} dörrar";
    }
}