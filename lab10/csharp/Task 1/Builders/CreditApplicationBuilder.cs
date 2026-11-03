using System;
using PrutCreditBank.Task1.Services;
using PrutCreditBank.Task1.Models;

namespace PrutCreditBank.Task1.Builders
{
    public class CreditApplicationBuilder : ICreditApplicationBuilder
    {
        private string _client;
        private decimal _amount;
        private int _termMonths;
        private string _purpose = "Споживчі потреби";
        private string _collateral;
        private string _guarantor;
        private bool _hasIncomeCertificate;
        private bool _hasInsurance;

        public ICreditApplicationBuilder SetClient(string client)
        {
            _client = client;
            Logger.Log("Builder", "Встановлено клієнта заявки.");
            return this;
        }

        public ICreditApplicationBuilder SetAmount(decimal amount)
        {
            _amount = amount;
            Logger.Log("Builder", $"Встановлено суму кредиту: {_amount} грн.");
            return this;
        }

        public ICreditApplicationBuilder SetTerm(int termMonths)
        {
            _termMonths = termMonths;
            Logger.Log("Builder", $"Встановлено строк кредиту: {_termMonths} місяців.");
            return this;
        }

        public ICreditApplicationBuilder SetPurpose(string purpose)
        {
            _purpose = purpose;
            Logger.Log("Builder", $"Встановлено мету кредиту: {_purpose}.");
            return this;
        }

        public ICreditApplicationBuilder SetCollateral(string collateral)
        {
            _collateral = collateral;
            Logger.Log("Builder", $"Додано заставу: {_collateral}.");
            return this;
        }

        public ICreditApplicationBuilder SetGuarantor(string guarantor)
        {
            _guarantor = guarantor;
            Logger.Log("Builder", $"Додано поручителя: {_guarantor}.");
            return this;
        }

        public ICreditApplicationBuilder SetIncomeCertificate(bool hasCertificate)
        {
            _hasIncomeCertificate = hasCertificate;
            Logger.Log("Builder", "Оновлено статус довідки про доходи.");
            return this;
        }

        public ICreditApplicationBuilder SetInsurance(bool hasInsurance)
        {
            _hasInsurance = hasInsurance;
            Logger.Log("Builder", "Оновлено статус страхування.");
            return this;
        }

        public CreditApplication Build()
        {
            if (string.IsNullOrWhiteSpace(_client))
            {
                Logger.Log("Validation", "Неможливо сформувати заявку: не вказано клієнта.");
                throw new InvalidOperationException("Неможливо сформувати заявку: не вказано клієнта.");
            }
            if (_amount <= 0)
            {
                Logger.Log("Validation", "Неможливо сформувати заявку: не вказано суму кредиту.");
                throw new InvalidOperationException("Неможливо сформувати заявку: не вказано суму кредиту.");
            }
            if (_termMonths <= 0)
            {
                Logger.Log("Validation", "Неможливо сформувати заявку: некоректний строк кредиту.");
                throw new InvalidOperationException("Неможливо сформувати заявку: некоректний строк кредиту.");
            }

            Logger.Log("Builder", "Заявка успішно пройшла перевірку та сформована.");
            return new CreditApplication(
                _client, _amount, _termMonths, _purpose, 
                _collateral, _guarantor, _hasIncomeCertificate, _hasInsurance);
        }
    }
}