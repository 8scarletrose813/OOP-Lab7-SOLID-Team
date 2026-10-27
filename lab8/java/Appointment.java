public class Appointment {
    private int appointmentId;
    private Patient patient;
    private Doctor doctor;
    private double consultationCost;
    private double insuranceCoverage;
    private double patientPayable;
    private String status;
    public Appointment(int appointmentId, Patient patient, Doctor doctor){
        this.appointmentId = appointmentId;
        this.patient = patient;
        this.doctor = doctor;
        this.consultationCost = doctor.calculateConsultationFee();
        this.insuranceCoverage = calculateInsuranceCoverage();
        this.patientPayable = this.consultationCost - this.insuranceCoverage;
        this.status = "Scheduled";
        this.confirm();
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

    private double calculateInsuranceCoverage(){
        if(patient.isHasInsurance()){
            return this.consultationCost * 0.80;
        }
        else {
            return 0.0;
        }
    }
    public String generateInvoice(){
        return "Квитанція прийому #" + appointmentId +
                "\nПацієнт: " + patient.getFullName() +
                "\nЛікар: " + doctor.getName() + " (" + doctor.getCategory() + ")" +
                "\nВартість: " + consultationCost + " грн" +
                "\nПокриття страховкою: " + insuranceCoverage + " грн" +
                "\nДо сплати пацієнтом: " + patientPayable + " грн" ;



    }
}