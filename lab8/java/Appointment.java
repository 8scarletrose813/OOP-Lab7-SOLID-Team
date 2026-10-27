public class Appointment {
    private int appointmentId;
    private Patient patient;
    private Doctor doctor;
    private double consultationCost;
    private double insuranceCoverage;
    private double patientPayable;
    private String status;
    public Appointment(int appointmentId, Patient patient, Doctor doctor, double consultationCost, double insuranceCoverage, double patientPayable, String status){
        this.appointmentId = appointmentId;
        this.patient = patient;
        this.doctor = doctor;
        this.consultationCost = consultationCost;
        this.insuranceCoverage = insuranceCoverage;
        this.patientPayable = patientPayable;
        this.status = "Scheduled";
    }
    public int getAppointmentId() {
        return appointmentId;
    }
    public Patient getPatient() {
        return patient;
    }
    public Doctor getDoctor() {
        return doctor;
    }
    public double getConsultationCost() {
        return consultationCost;
    }
    public double getInsuranceCoverage() {
        return insuranceCoverage;
    }
    public double getPatientPayable() {
        return patientPayable;
    }
    public String getStatus() {
        return status;
    }
    public void confirm(){
        if ("Cancelled".equalsIgnoreCase(this.status)){
            throw new IllegalStateException("Неможливо підтвердити скасований прийом");
        }
        this.status = "Confirmed";
    }
    public void cancel() {
        this.status = "Cancelled";
    }
}