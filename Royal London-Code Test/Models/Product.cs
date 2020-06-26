
namespace Royal_London_Code_Test.Models
{
    public class Product : IProduct
    {
        #region Fields

        #endregion

        #region Constructors

        public Product(string productName, decimal payoutAmount, decimal annualPremium)
        {
            ProductName = productName;
            PayoutAmount = payoutAmount;
            AnnualPremium = annualPremium;
        }
        #endregion

        #region Properties

        public string ProductName { get; set; }

        public decimal PayoutAmount { get; set; }

        public decimal AnnualPremium { get; set; }

        public decimal CreditCharge { get; set; }

        public decimal TotalDebitPremium { get; set; }

        public decimal AverageMonthlyPremium { get; set; }

        public decimal InitialMonthlyPaymentAmount { get; set; }

        public decimal OtherMonthlyPaymentAmount { get; set; }
        #endregion
    }
}