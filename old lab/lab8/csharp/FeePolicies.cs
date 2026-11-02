// Polymorphism: кожна категорія лікаря — окрема реалізація контракту замість каскаду if-else.
public class GeneralFeePolicy : IConsultationFeePolicy
{
    public string CategoryName => "General";
    public double Calculate(double baseFee) => baseFee;
}

public class SpecialistFeePolicy : IConsultationFeePolicy
{
    public string CategoryName => "Specialist";
    public double Calculate(double baseFee) => baseFee * 1.4;
}

public class ProfessorFeePolicy : IConsultationFeePolicy
{
    public string CategoryName => "Professor";
    public double Calculate(double baseFee) => baseFee * 2.0;
}
