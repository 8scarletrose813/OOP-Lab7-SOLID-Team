using System;

public class SmsNotificationService : NotificationService
{
    public void SendSms(Patient patient, Appointment appointment)
    {
        string phone = patient.Phone;
        Console.WriteLine("[SMS Gateway] Надіслано SMS на номер: " + phone);
    }
}