using PRG_MAUI_Car_Register.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PRG_MAUI_Car_Register.Services
{
    public class DataService
    {
        private string filePath = Path.Combine(FileSystem.AppDataDirectory, "vehicles.json");

        public void Save(List<Vehicle> vehicles)
        {
            var json = JsonSerializer.Serialize(vehicles, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
            Debug.WriteLine("Sparar JSON...");
        }

        public List<Vehicle> Load()
        {
            if (!File.Exists(filePath))
                return new List<Vehicle>();

            var json = File.ReadAllText(filePath);

            var tempList = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

            var result = new List<Vehicle>();

            foreach (var item in tempList)
            {
                var type = item["Type"].ToString();

                switch (type)
                {
                    case "Car":
                        result.Add(JsonSerializer.Deserialize<Car>(JsonSerializer.Serialize(item)));
                        break;

                    case "Motorcycle":
                        result.Add(JsonSerializer.Deserialize<Motorcycle>(JsonSerializer.Serialize(item)));
                        break;

                    case "Truck":
                        result.Add(JsonSerializer.Deserialize<Truck>(JsonSerializer.Serialize(item)));
                        break;
                }
            }
            Debug.WriteLine("Försöker ladda JSON...");
            return result;
        }
    }

}