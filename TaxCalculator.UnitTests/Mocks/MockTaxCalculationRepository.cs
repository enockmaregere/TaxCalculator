using System.Collections.Generic;
using System.Threading.Tasks;

using TaxCalculator.Interfaces;
using TaxCalculator.Models;
using TaxCalculator.RESTAPI.Constants;

namespace TaxCalculator.UnitTests.Mocks
{
    public class MockTaxCalculationRepository : ITaxCalculationRepository
    {
        public Task<List<TaxBracket>> GetProgressiveTaxBrackets()
        {
            List<TaxBracket> taxBrackets = new List<TaxBracket> {
               new TaxBracket
               {
                   Rate = 10,
                   From = 0,
                   To = 8350
               },

               new TaxBracket
               {
                   Rate = 15,
                   From = 8351,
                   To = 33950
               },

               new TaxBracket
               {
                   Rate = 25,
                   From = 33951,
                   To = 82250
               },

               new TaxBracket
               {
                   Rate = 28,
                   From = 82251,
                   To = 171550
               },

               new TaxBracket
               {
                   Rate = 33,
                   From = 171551,
                   To = 372950
               },

               new TaxBracket
               {
                   Rate = 35,
                   From = 372951,
                   To = decimal.MaxValue
               }
            };

            return Task.FromResult(taxBrackets);
        }

        public Task<List<TaxCalculationType>> GetTaxCalculationTypes()
        {
            List<TaxCalculationType> taxCalculationTypes = new List<TaxCalculationType>()
            {
                new TaxCalculationType
                {
                    PostalCode = PostalCodes.PostalCode7441,
                    CalculationType = TaxTypes.Progressive
                },

                new TaxCalculationType
                {
                    PostalCode = PostalCodes.PostalCodeA100,
                    CalculationType = TaxTypes.FlatValue
                },

                new TaxCalculationType
                {
                    PostalCode = PostalCodes.PostalCode7000,
                    CalculationType = TaxTypes.FlatRate
                },

                new TaxCalculationType
                {
                    PostalCode = PostalCodes.PostalCode1000,
                    CalculationType = TaxTypes.Progressive
                }
            };

            return Task.FromResult(taxCalculationTypes);
        }

        public Task<TaxFlatRate> GetTaxFlatRate()
        {
            return Task.FromResult(new TaxFlatRate()
            {
                Rate = 17.5M
            });
        }

        public Task<TaxFlatValue> GetTaxFlatValue()
        {
            return Task.FromResult(new TaxFlatValue()
            {
                Rate = 5,
                FlatValue = 10000,
                IncomeThreshold = 200000
            });
        }

        public Task SaveCalculatedTax(TaxCalculationResult taxCalculationResult)
        {
            return Task.FromResult(0);
        }
    }
}
