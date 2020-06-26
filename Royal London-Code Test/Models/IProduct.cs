namespace Royal_London_Code_Test.Models
{
    public interface IProduct
    {
        string ProductName { get; }

        decimal PayoutAmount { get; }

        decimal AnnualPremium { get; }

        decimal CreditCharge { get; set; }

        decimal TotalDebitPremium { get; set; }

        decimal AverageMonthlyPremium { get; set; }

        decimal InitialMonthlyPaymentAmount { get; set; }

        decimal OtherMonthlyPaymentAmount { get; set; }
    }
}