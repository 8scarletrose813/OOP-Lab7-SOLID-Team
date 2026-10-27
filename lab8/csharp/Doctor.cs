public class Doctor
{
    public int Id { get; }
    public string Name { get; }
    public string Specialty { get; }
    public string Category { get; } // "General", "Specialist", "Professor"
    public double BaseConsultationFee { get; }

    public Doctor(int id, string name, string specialty, string category, double baseConsultationFee)
    {
        Id = id;
        Name = name;
        Specialty = specialty;
        Category = category;
        BaseConsultationFee = baseConsultationFee;
    }

    public double CalculateConsultationFee()
    {
        if (string.Equals("Professor", Category, System.StringComparison.OrdinalIgnoreCase))
        {
            return BaseConsultationFee * 2.0;
        }
        else if (string.Equals("Specialist", Category, System.StringComparison.OrdinalIgnoreCase))
        {
            return BaseConsultationFee * 1.4;
        }
        else
        {
            return BaseConsultationFee;
        }
    }
}