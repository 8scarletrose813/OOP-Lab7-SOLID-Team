
public class Doctor {
    private int id;
    private String name;
    private String specialty;
    private String category; // "General", "Specialist", "Professor"
    private double baseConsultationFee;
    public Doctor(int id, String name, String specialty, String category, double baseConsultationFee){
        this.id = id;
        this.name =name;
        this.specialty = specialty;
        this.category = category;
        this.baseConsultationFee = baseConsultationFee;
    }
    public int getId() {
        return id;
    }
    public String getName() {
        return name;
    }
    public String getSpecialty() {
        return specialty;
    }
    public String getCategory() {
        return category;
    }
    public double getBaseConsultationFee() {
        return baseConsultationFee;
    }
    public double calculateConsultationFee(){
        if("Professor".equalsIgnoreCase(category)){
            return baseConsultationFee * 2.0;
        }
        else if("Specialist".equalsIgnoreCase(category)){
            return baseConsultationFee * 1.4;
        }
        else{
            return baseConsultationFee;
        }
    }
}