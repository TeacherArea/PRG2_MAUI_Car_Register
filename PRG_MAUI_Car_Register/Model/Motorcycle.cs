using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_MAUI_Car_Register.Model
{
    public class Motorcycle : Vehicle
    {
        public bool HasSidecar { get; set; }

        public Motorcycle(string registrationNumber, string manufacturer, string model, string yearModel, bool hasSidecar)
            : base(registrationNumber, manufacturer, model, yearModel)
        {
            HasSidecar = hasSidecar;
        }

        public override string GetDescription() =>
            $"{RegistrationNumber}\tMC\t{Manufacturer}\t{Model}\t{YearModel}" +
            (HasSidecar ? "\tMed sidovagn" : "");
    }
}
