using System;
using System.Collections.Generic;

// Pure Fabrication + Dictionary Dispatch: єдине місце, що знає відповідність "назва категорії -> політика".
// Нова категорія = новий клас політики + один рядок реєстрації; Doctor та Appointment не змінюються.
public class ConsultationFeePolicyFactory
{
    private readonly Dictionary<string, IConsultationFeePolicy> _policies;
    private readonly IConsultationFeePolicy _defaultPolicy;

    public ConsultationFeePolicyFactory()
    {
        _defaultPolicy = new GeneralFeePolicy();
        _policies = new Dictionary<string, IConsultationFeePolicy>(StringComparer.OrdinalIgnoreCase);
        Register(_defaultPolicy);
        Register(new SpecialistFeePolicy());
        Register(new ProfessorFeePolicy());
    }

    // Невідома категорія -> базовий тариф (збережено поведінку гілки else з вихідного коду).
    public IConsultationFeePolicy Create(string category)
    {
        return _policies.TryGetValue(category ?? string.Empty, out IConsultationFeePolicy policy)
            ? policy
            : _defaultPolicy;
    }

    private void Register(IConsultationFeePolicy policy)
    {
        _policies[policy.CategoryName] = policy;
    }
}
