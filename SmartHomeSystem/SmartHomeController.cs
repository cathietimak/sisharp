namespace SmartHomeSystem;

public class SmartHomeController
{
    private readonly List<ISwitchable> AllDevices = new List<ISwitchable>(); 
    private readonly List<IEnergyConsumer> EnergyConsumers = new List<IEnergyConsumer>(); 

    public void AddDevice(ISwitchable device)
    {
        AllDevices.Add(device);
    }

    public void AddEnergyDevice(IEnergyConsumer device)
    {
        EnergyConsumers.Add(device);
    }

    public void TurnAllOn()
    {
        foreach (var device in AllDevices)
        {
            device.TurnOn();
        }
    }

    public void TurnAllOff()
    {
        foreach (var device in AllDevices)
        {
            device.TurnOff();
        }
    }

    public void ShowEnergyReport(int hours)
    {
        double totalEnergyUsage = 0.0;
        double costPerKWh = 4.0;

        Console.WriteLine($"\nЗвіт про споживання енергії за {hours} год:");

        foreach (var device in EnergyConsumers)
        {
            double energyUsage = device.GetEnergyUsage(hours);
            totalEnergyUsage += energyUsage; 
            
            Console.WriteLine($"{device.DeviceName}: {energyUsage:F2} кВт·год (потужність: {device.PowerConsumption} Вт)");
        }

        double totalCost = totalEnergyUsage * costPerKWh;
        Console.WriteLine($"Загальне споживання: {totalEnergyUsage:F2} кВт·год");
        Console.WriteLine($"Вартість (~{costPerKWh} грн/кВт·год): {totalCost:F2} грн");
    }

    public IEnumerable<Device> GetAllDevices()
    {
        List<Device> deviceList = new List<Device>();

        foreach (var device in AllDevices)
        {
            if (device is Device specificDevice)
            {
                deviceList.Add(specificDevice);
            }
        }
        return deviceList;
    }
}