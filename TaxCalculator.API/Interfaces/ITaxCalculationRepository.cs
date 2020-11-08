using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaxCalculator.Interfaces
{
    public interface ITaxCalculationRepository
    {
        Task<List<Models.TaxCalculationType>> GetTaxCalculationTypes();
        Task<List<Models.TaxBracket>> GetProgressiveTaxBrackets();
        Task<Models.TaxFlatValue> GetTaxFlatValue();
        Task<Models.TaxFlatRate> GetTaxFlatRate();
        Task SaveCalculatedTax(Models.TaxCalculationResult taxCalculationResult);
    }
}
