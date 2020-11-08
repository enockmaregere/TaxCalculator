using System.Threading.Tasks;

namespace TaxCalculator.Interfaces
{
    public interface ITaxCalculator
    {
        public Task<decimal> CalculateTax(decimal annualIncome);
    }
}
