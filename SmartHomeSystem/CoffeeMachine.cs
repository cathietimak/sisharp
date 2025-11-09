namespace SmartHomeSystem;
public class CoffeeMachine : Device, IEnergyConsumer
{
    private const int Power = 1000;

    public string DeviceName => Name;
    public int PowerConsumption => Power;

    public override void TurnOn()
    {
        IsOn = true;
        Console.WriteLine($"{Name} почала готувати каву.");
    }

    public override void TurnOff()
    {
        IsOn = false;
        Console.WriteLine($"{Name} завершила роботу.");
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