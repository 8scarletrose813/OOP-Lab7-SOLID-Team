public class Main {
    public static void main(String[] args){
        FileAppointmentRepository fileAppointmentRepository = new FileAppointmentRepository();
        SmsNotificationService smsNotificationService = new SmsNotificationService();
        ClinicAppointmentService clinicAppointmentService = new ClinicAppointmentService( fileAppointmentRepository, smsNotificationService);
        Patient patient = new Patient(101, "Іван Петренко", true, "INS-998877", "+380971234567");
        Doctor doctor =new Doctor(501, "Д-р Олексій Коваль", "Вища", "Professor", 800.0);
        Appointment appointment = clinicAppointmentService.scheduleAppointment(3001, patient, doctor);
        System.out.println(appointment.generateInvoice());
    }
}
