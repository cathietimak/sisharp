using System;

namespace HospitalManagementSystem;

public class HospitalDemo
{
    public void Run()
    {
        Console.WriteLine("=== HOSPITAL MANAGEMENT SYSTEM ===\n");

        Hospital hospital = new Hospital();

        var doctor1 = new Doctor(1, "Dr. Hippocrates", "Ancient Medicine");
        var doctor2 = new Doctor(2, "Dr. Galen", "Surgery");
        var doctor3 = new Doctor(3, "Dr. Avicenna", "Internal Medicine");

        hospital.AddDoctor(doctor1);
        hospital.AddDoctor(doctor2);
        hospital.AddDoctor(doctor3);

        var patient1 = new Patient(1, "Cleopatra", 35);
        var patient2 = new Patient(2, "Julius Caesar", 55);
        var patient3 = new Patient(3, "Joan of Arc", 19);
        var patient4 = new Patient(4, "Leonardo da Vinci", 67);

        hospital.RegisterPatient(patient1);
        hospital.RegisterPatient(patient2);
        hospital.RegisterPatient(patient3);
        hospital.RegisterPatient(patient4);

        var room1 = new HospitalRoom(101, 2);
        var room2 = new HospitalRoom(102, 1);
        var room3 = new HospitalRoom(103, 3);

        hospital.CreateRoom(room1);
        hospital.CreateRoom(room2);
        hospital.CreateRoom(room3);

        hospital.HospitalizePatient(1, 101);
        hospital.HospitalizePatient(2, 101);
        hospital.HospitalizePatient(3, 102);
        hospital.HospitalizePatient(4, 101); //(should fail)
        hospital.HospitalizePatient(4, 103);
        hospital.HospitalizePatient(5, 101); // (should fail)
        hospital.HospitalizePatient(1, 999); // (should fail)

        var record1 = new MedicalRecord(patient1, doctor1, DateTime.Now.AddDays(-10), "Pharaoh's ailment treated with herbs");
        var record2 = new MedicalRecord(patient2, doctor2, DateTime.Now.AddDays(-5), "Stab wound from Ides of March");
        var record3 = new MedicalRecord(patient3, doctor3, DateTime.Now.AddDays(-3), "Battle fatigue and visions");

        hospital.AddMedicalRecord(record1);
        hospital.AddMedicalRecord(record2);
        hospital.AddMedicalRecord(record3);

        Console.WriteLine("\n--- PATIENT HISTORY ---");
        var history = hospital.GetPatientHistory(1);
        foreach (var record in history)
        {
            Console.WriteLine($"  Date: {record.Date.ToShortDateString()}");
            Console.WriteLine($"  Doctor: {record.Doctor.Name}");
            Console.WriteLine($"  Description: {record.Description}\n");
        }

        Console.WriteLine(hospital.GetStatistics());
    }
}