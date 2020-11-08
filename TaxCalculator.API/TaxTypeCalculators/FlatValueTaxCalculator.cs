using System;
using System.Threading.Tasks;

using TaxCalculator.Interfaces;

namespace TaxCalculator.TaxCalculators
{
    public class FlatValueTaxCalculator : ITaxCalculator
    {
        private readonly ITaxCalculationRepository _taxCalculationRepository;

        public FlatValueTaxCalculator(ITaxCalculationRepository taxCalculationRepository)
        {
            _taxCalculationRepository = taxCalculationRepository;
        }

        public async Task<decimal> CalculateTax(decimal annualIncome)
        {
            var taxFlatValues = await _taxCalculationRepository.GetTaxFlatValue();

            if (annualIncome < taxFlatValues.IncomeThreshold)
            {
                return Math.Round(annualIncome * (taxFlatValues.Rate / 100), 2);
            }

            return Math.Round(taxFlatValues.FlatValue, 2);
        }
    }
}
