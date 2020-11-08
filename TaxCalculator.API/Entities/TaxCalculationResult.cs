using System;

namespace TaxCalculator.Entities
{
    public class TaxCalculationResult
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string PostalCode { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal CalculatedTax { get; set; }
    }
}
