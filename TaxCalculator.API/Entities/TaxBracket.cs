namespace TaxCalculator.Entities
{
    public class TaxBracket
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public decimal From { get; set; }
        public decimal To { get; set; }
    }
}
