namespace SmartHomeSystem;
public class Light : Device, IEnergyConsumer
{
    private const int Power = 60;

    public string DeviceName => Name;
    public int PowerConsumption => Power;

    public override void TurnOn()
    {
        IsOn = true;
        Console.WriteLine($"{Name} засвітилася.");
    }

    public override void TurnOff()
    {
        IsOn = false;
        Console.WriteLine($"{Name} вимкнена.");
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