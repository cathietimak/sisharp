namespace HospitalManagementSystem;

public class HospitalRoom
{
    public int RoomNumber { get; set; }
    public int Capacity { get; set; }
    public List<Patient> Patients { get; set; }

    public HospitalRoom(int roomNumber, int capacity)
    {
        RoomNumber = roomNumber;
        Capacity = capacity;
        Patients = new List<Patient>();
    }

    public void AddPatient(Patient patient)
    {
        if (Patients.Count < Capacity)
        {
            Patients.Add(patient);
        }
        else
        {
            Console.WriteLine("Палата переповнена");
        }
    }
}