namespace VatCalculator.Contracts.Calculation
{
    public class CalculationResponse
    {
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal VatAmount { get; set; }
    }
}
