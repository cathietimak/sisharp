namespace SmartHomeSystem;
public abstract class Device : ISwitchable
{
    public required string Name { get; set; }
    public bool IsOn { get; protected set; }

    public abstract void TurnOn();
    public abstract void TurnOff();

    public void PrintStatus()
    {
        string status;
        if (IsOn)
        {
            status = "увімкнено";
        }
        else
        {
            status = "вимкнено";
        }
        Console.WriteLine($"{Name}: {status}");
    }
}