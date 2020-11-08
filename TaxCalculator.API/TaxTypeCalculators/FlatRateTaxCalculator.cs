using System;
using System.Threading.Tasks;

using TaxCalculator.Interfaces;

namespace TaxCalculator.TaxCalculators
{
    public class FlatRateTaxCalculator : ITaxCalculator
    {
        private readonly ITaxCalculationRepository _taxCalculationRepository;

        public FlatRateTaxCalculator(ITaxCalculationRepository taxCalculationRepository)
        {
            _taxCalculationRepository = taxCalculationRepository;
        }

        public async Task<decimal> CalculateTax(decimal annualIncome)
        {
            var taxFlatRate = await _taxCalculationRepository.GetTaxFlatRate();

            return Math.Round((taxFlatRate.Rate / 100) * annualIncome, 2);
        }
    }
}
