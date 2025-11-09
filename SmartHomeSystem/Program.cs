namespace SmartHomeSystem;
public class Program
{
    public static void Main()
    {
        var controller = new SmartHomeController();

        var livingRoomLight = new Light { Name = "Лампа у вітальні засвітилася" };
        var bedroomAC = new AirConditioner { Name = "Кондиціонер у спальні почав охолодження" };
        var kitchenCoffeeMachine = new CoffeeMachine { Name = "Кавомашина на кухні почала готувати каву" };
        var hallMotionSensor = new MotionSensor { Name = "Датчик руху у коридорі активовано" };

        controller.AddDevice(livingRoomLight);
        controller.AddDevice(bedroomAC);
        controller.AddDevice(kitchenCoffeeMachine);
        controller.AddDevice(hallMotionSensor);

        controller.AddEnergyDevice(livingRoomLight);
        controller.AddEnergyDevice(bedroomAC);
        controller.AddEnergyDevice(kitchenCoffeeMachine);

        Console.WriteLine("Увімкнення всіх пристроїв");
        controller.TurnAllOn();

        Console.WriteLine("\nСтатус пристроїв");
        foreach (var device in controller.GetAllDevices())
        {
            device.PrintStatus();
        }

        controller.ShowEnergyReport(5);

        Console.WriteLine("\nВимкнення всіх пристроїв");
        controller.TurnAllOff();
    }
}