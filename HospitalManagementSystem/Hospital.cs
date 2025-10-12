namespace HospitalManagementSystem;

public class Hospital
{
    public List<Doctor> Doctors { get; set; }
    public List<Patient> Patients { get; set; }
    public List<HospitalRoom> Rooms { get; set; }
    public List<MedicalRecord> Records { get; set; }

    public Hospital()
    {
        Doctors = new List<Doctor>();
        Patients = new List<Patient>();
        Rooms = new List<HospitalRoom>();
        Records = new List<MedicalRecord>();
    }

    public void AddDoctor(Doctor doctor)
    {
        Doctors.Add(doctor);
    }

    public void RegisterPatient(Patient patient)
    {
        Patients.Add(patient);
    }

    public void CreateRoom(HospitalRoom room)
    {
        Rooms.Add(room);
    }

    public void HospitalizePatient(int patientId, int roomNumber)
    {
        var patient = Patients.FirstOrDefault(p => p.Id == patientId);
        var room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
        if (patient != null && room != null && room.Patients.Count < room.Capacity)
        {
            room.AddPatient(patient);
        }
    }

    public void AddMedicalRecord(MedicalRecord record)
    {
        Records.Add(record);
    }

    public List<MedicalRecord> GetPatientHistory(int patientId)
    {
        return Records.Where(r => r.Patient.Id == patientId).ToList();
    }

    public string GetStatistics()
    {
        int totalPatientsInRooms = Rooms.Sum(r => r.Patients.Count);
        return $"СТАТИСТИКА\n" +
               $"Кількість лікарів: {Doctors.Count}\n" +
               $"Кількість пацієнтів: {Patients.Count}\n" +
               $"Кількість палат: {Rooms.Count}\n" +
               $"Кількість пацієнтів у палатах: {totalPatientsInRooms}";
    }
}