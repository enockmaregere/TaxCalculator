namespace TaxCalculator.Interfaces
{
    public interface ITaxCalculatorFactory
    {
        public ITaxCalculator GetTaxCalculator(string taxType);
    }
}
