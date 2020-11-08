namespace TaxCalculator.Entities
{
    public class TaxFlatValue
    {
        public int Id { get; set; }
        public decimal FlatValue { get; set; }
        public decimal IncomeThreshold { get; set; }
        public decimal Rate { get; set; }
    }
}
