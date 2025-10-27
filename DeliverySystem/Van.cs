using System;

namespace DeliverySystem
{
    public class Van : Car
    {
        private double loadCapacity;
        private double currentLoad;

        public Van(string brand, int year, double mileage, int doors, double loadCapacity)
            : base(brand, year, mileage, doors, 140.0)
        {
            this.loadCapacity = loadCapacity;
            this.currentLoad = 0;
        }

        public override string GetInfo()
        {
            return $"Van: {brand} ({year}), Doors: {doors}, Load: {currentLoad}/{loadCapacity}kg, Fuel: {fuelLevel}L";
        }

        public void LoadCargo(double weight)
        {
            if (currentLoad + weight <= loadCapacity)
            {
                currentLoad += weight;
                Console.Write($"{weight} kg loaded into the van.\r\n");
            }
            else
            {
                Console.Write("Too heavy! Cannot load more cargo.\r\n");
            }
        }

        public void UnloadCargo()
        {
            currentLoad = 0;
            Console.Write("Van unloaded.\r\n");
        }
    }
}