// Pure Fabrication: формування квитанції — відповідальність представлення, а не доменної сутності.
// Винесення з Appointment усуває другу компоненту зв'язності (LCOM4: 2 -> 1).
public class InvoiceGenerator
{
    public string Generate(Appointment appointment)
    {
        return "Квитанція прийому #" + appointment.AppointmentId +
                "\nПацієнт: " + appointment.PatientName +
                "\nЛікар: " + appointment.DoctorName + " (" + appointment.DoctorCategory + ")" +
                "\nВартість: " + appointment.ConsultationCost + " грн" +
                "\nПокриття страховкою: " + appointment.InsuranceCoverage + " грн" +
                "\nДо сплати пацієнтом: " + appointment.PatientPayable + " грн";
    }
}
