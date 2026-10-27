public class SmsNotificationService implements NotificationService{
    @Override
    public void sendSms(Patient patient, Appointment appointment) {
        String phone = patient.getPhone();
        System.out.println("[SMS Gateway] Надіслано SMS на номер: " + phone );

    }
}
