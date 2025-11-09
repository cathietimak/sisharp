namespace SmartHomeSystem;
public class AirConditioner : Device, IEnergyConsumer
{
    private const int Power = 2000;

    public string DeviceName => Name;
    public int PowerConsumption => Power;

    public override void TurnOn()
    {
        IsOn = true;
        Console.WriteLine($"{Name} почав охолодження.");
    }

    public override void TurnOff()
    {
        IsOn = false;
        Console.WriteLine($"{Name} зупинено.");
    }

    public double GetEnergyUsage(int hours)
    {
        if (IsOn == false)
        {
            return 0.0;
        }
        return (double)PowerConsumption * hours / 1000.0;
    }
}