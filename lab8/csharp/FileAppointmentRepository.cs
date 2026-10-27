using System;

public class FileAppointmentRepository : AppointmentRepository
{
    public Appointment Save(Appointment appointment)
    {
        int appointmentId = appointment.AppointmentId;
        Console.WriteLine("[Електронна карта] Запис #" + appointment.AppointmentId + " збережено у файл appointments.log.");
        return appointment;
    }
}