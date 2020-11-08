using System;
using System.Threading.Tasks;

using TaxCalculator.Interfaces;

namespace TaxCalculator.TaxCalculators
{
    public class ProgressiveTaxCalculator : ITaxCalculator
    {
        private readonly ITaxCalculationRepository _taxCalculationRepository;

        public ProgressiveTaxCalculator(ITaxCalculationRepository taxCalculationRepository)
        {
            _taxCalculationRepository = taxCalculationRepository;
        }

        public async Task<decimal> CalculateTax(decimal annualIncome)
        {
            var taxBrackets = await _taxCalculationRepository.GetProgressiveTaxBrackets();

            taxBrackets[^1].To = decimal.MaxValue;

            decimal taxPayable = 0;

            for (int i = 1; i < taxBrackets.Count; i++)
            {
                if (annualIncome <= taxBrackets[0].To)
                {
                    taxPayable = annualIncome * (taxBrackets[0].Rate / 100);
                    break;
                }

                taxPayable += (taxBrackets[i - 1].Rate / 100) * (taxBrackets[i - 1].To - (taxBrackets[i - 1].From));

                if (annualIncome >= taxBrackets[i].From && annualIncome <= taxBrackets[i].To)
                {
                    taxPayable += (taxBrackets[i].Rate / 100) * (annualIncome - (taxBrackets[i].From - 1));
                    break;
                }
            }

            return Math.Round(taxPayable, 2);
        }
    }
}
