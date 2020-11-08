using System;

namespace TaxCalculator.Models
{
    public class TaxCalculationResult
    {
        public DateTime Date { get; set; }
        public string PostalCode { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal CalculatedTax { get; set; }
    }
}
