public class NoInsurancePolicy : IInsurancePolicy
{
    public double CalculateCoverage(double consultationCost) => 0.0;
}

public class StandardInsurancePolicy : IInsurancePolicy
{
    private const double CoverageRate = 0.80;

    public string PolicyNumber { get; }

    public StandardInsurancePolicy(string policyNumber)
    {
        PolicyNumber = policyNumber;
    }

    public double CalculateCoverage(double consultationCost) => consultationCost * CoverageRate;
}
