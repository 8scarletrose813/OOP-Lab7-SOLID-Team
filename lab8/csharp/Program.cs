using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Composition root: єдине місце, де вибираються конкретні реалізації.
        FileAppointmentRepository fileAppointmentRepository = new FileAppointmentRepository();
        SmsNotificationService smsNotificationService = new SmsNotificationService();
        ClinicAppointmentService clinicAppointmentService = new ClinicAppointmentService(fileAppointmentRepository, smsNotificationService);
        ConsultationFeePolicyFactory feePolicyFactory = new ConsultationFeePolicyFactory();
        InvoiceGenerator invoiceGenerator = new InvoiceGenerator();

        Patient patient = new Patient(101, "Іван Петренко", new StandardInsurancePolicy("INS-998877"), "+380971234567");
        Doctor doctor = new Doctor(501, "Д-р Олексій Коваль", "Вища", feePolicyFactory.Create("Professor"), 800.0);

        Appointment appointment = clinicAppointmentService.ScheduleAppointment(new ScheduleAppointmentCommand(3001, patient, doctor));
        Console.WriteLine(invoiceGenerator.Generate(appointment));
    }
}
