using TaxCalculator.Interfaces;
using TaxCalculator.RESTAPI.Constants;
using TaxCalculator.TaxCalculators;

namespace TaxCalculator.Factories
{
    public class TaxCalculatorFactory : ITaxCalculatorFactory
    {
        private readonly ITaxCalculationRepository _taxCalculationRepository;

        public TaxCalculatorFactory(ITaxCalculationRepository taxCalculationRepository)
        {
            _taxCalculationRepository = taxCalculationRepository;
        }

        public ITaxCalculator GetTaxCalculator(string taxType)
        {
            return taxType switch
            {
                TaxTypes.Progressive => new ProgressiveTaxCalculator(_taxCalculationRepository),
                TaxTypes.FlatValue => new FlatValueTaxCalculator(_taxCalculationRepository),
                TaxTypes.FlatRate => new FlatRateTaxCalculator(_taxCalculationRepository),
                _ => null,
            };
        }
    }
}
